import { Component, OnInit, ViewChild } from "@angular/core";
import { first } from "rxjs/operators";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormGroupName,
} from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { School, List } from "../../../../../models";
import { TeacherService, AlertService } from "../../../../../services";
import { STATUS } from "../../../../../enums";

@Component({
  selector: "app-edit-teacher",
  templateUrl: "./edit-teacher.component.html",
  styleUrls: ["./edit-teacher.component.css"],
})
export class EditTeacherComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;

  public selectedModel;
  public statusList = Object.values(STATUS);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.resetForm();
    this.teacherService.currentSelectedModel.subscribe(
      (model) => (this.selectedModel = model)
    );
    if (this.selectedModel) {
      this.modelForm.patchValue(this.selectedModel);
    }
  }

  get f() {
    return this.modelForm.controls;
  }

  resetForm() {
    this.modelForm = this.formBuilder.group({
      name: ["", Validators.required],
      address: ["", Validators.required],
      contactNumber: ["", Validators.required],
      emailAddress: ["", [Validators.required, Validators.email]],
      schoolId: [],
      // userName: ["", Validators.required],
      active: [""],
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

    this.selectedModel.name = this.f.name.value;
    this.selectedModel.emailAddress = this.f.emailAddress.value;
    this.selectedModel.contactNumber = this.f.contactNumber.value;
    // this.selectedModel.userName = this.f.userName.value;
    this.selectedModel.active = this.f.active.value;

    this.loading = true;
    this.teacherService
      .udpate(this.selectedModel)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Update successful.", {
            autoClose: true,
            keepAfterRouteChange: true,
          });
          this.router.navigate(["/settings/teacher"]);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
