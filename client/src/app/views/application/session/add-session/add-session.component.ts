import { Component, OnInit, ViewChild } from "@angular/core";
import { first } from "rxjs/operators";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormGroupName,
} from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { List } from "../../../../models";
import {
  SessionService,
  ListService,
  AlertService,
  AuthenticationService,
} from "../../../../services";
import { EnumRole } from "../../../../enums";

@Component({
  selector: "app-add-session",
  templateUrl: "./add-session.component.html",
  styleUrls: ["./add-session.component.css"],
})
export class AddSessionComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public isSuperAdmin: boolean = false;
  public isTeacher: boolean = false;
  public allList: any;
  public classList: List[];
  public sectionList: List[];
  public teacherList: List[];
  public bsValue: Date = new Date();
  public mytime: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private sessionService: SessionService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private listService: ListService
  ) {}

  ngOnInit(): void {
    this.resetForm();
    this.getList();
    let currentUser = this.authenticationService.currentUserValue;
    if (currentUser.userRole === EnumRole.SuperAdmin.toString()) {
      this.isSuperAdmin = true;
    }
    if (currentUser.userRole === EnumRole.Teacher.toString()) {
      this.isTeacher = true;
    }
  }

  get f() {
    return this.modelForm.controls;
  }

  resetForm() {
    this.modelForm = this.formBuilder.group({
      sessionTitle: ["", Validators.required],
      sessionDesc: ["", Validators.required],
      classId: ["", Validators.required],
      teacherId: ["", Validators.required],
      schoolId: [],
      scheduledDate: ["", Validators.required],
      startingTime: ["", Validators.required],
      endingTime: ["", [Validators.required]],
      sessionStatusId: ["", Validators.required],
    });
  }

  getList() {
    this.loading = true;
    this.listService.getAll().subscribe((response) => {
      this.loading = false;
      this.allList = response;
      this.classList = this.allList[1];
      this.sectionList = this.allList[2];
      this.teacherList = this.allList[4];
    });
  }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.modelForm.invalid) {
      return;
    }

    if (!this.isSuperAdmin) {
      this.modelForm.value.schoolId = this.authenticationService.currentUserValue.schoolId.toString();
    }

    this.loading = true;
    this.sessionService
      .add(this.modelForm.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Session created successfully.", {
            autoClose: true,
            keepAfterRouteChange: true,
          });
          this.router.navigate(["/session"]);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
