import { Component, OnInit, ViewChild } from "@angular/core";
import { first } from "rxjs/operators";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormGroupName,
} from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { List } from "../../../../../models";
import {
  StudentService,
  ListService,
  AlertService,
  AuthenticationService,
} from "../../../../../services";
import { EnumRole } from "../../../../../enums";

@Component({
  selector: "app-add-student",
  templateUrl: "./add-student.component.html",
  styleUrls: ["./add-student.component.css"],
})
export class AddStudentComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public isSuperAdmin: boolean = false;
  public allList: any;
  public classList: List[];
  public sectionList: List[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private studentService: StudentService,
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
  }

  get f() {
    return this.modelForm.controls;
  }

  resetForm() {
    this.modelForm = this.formBuilder.group({
      name: ["", Validators.required],
      classId: ["", Validators.required],
      sectionId: ["", Validators.required],
      schoolId: [],
      userName: ["", Validators.required],
      rollNumber: ["", Validators.required],
      parentEmailAddress: ["", [Validators.email]],
      primaryContanctNo: ["", Validators.required],
      secondaryContactNo: [],
      parentName: ["", Validators.required],
      email: ["", [Validators.email]],
    });
  }

  getList() {
    this.loading = true;
    this.listService.getAll().subscribe((response) => {
      this.loading = false;
      this.allList = response;
      this.classList = this.allList[1];
      this.sectionList = this.allList[2];
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
    this.studentService
      .add(this.modelForm.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Student created successfully.", {
            autoClose: true,
            keepAfterRouteChange: true,
          });
          this.router.navigate(["/settings/student"]);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
