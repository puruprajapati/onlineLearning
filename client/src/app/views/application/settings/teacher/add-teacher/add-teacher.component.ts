import { Component, OnInit, ViewChild } from "@angular/core";
import { first } from "rxjs/operators";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormGroupName,
} from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { UploadResponse } from "../../../../../models";
import {
  TeacherService,
  AlertService,
  AuthenticationService,
} from "../../../../../services";
import { EnumRole } from "../../../../../enums";

import { environment } from "../../../../../../environments/environment";

@Component({
  selector: "app-add-teacher",
  templateUrl: "./add-teacher.component.html",
  styleUrls: ["./add-teacher.component.css"],
})
export class AddTeacherComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public isSuperAdmin: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.resetForm();
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
      address: ["", Validators.required],
      contactNumber: ["", Validators.required],
      emailAddress: ["", [Validators.required, Validators.email]],
      schoolId: [],
      userName: ["", Validators.required],
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

    console.log("superadin", this.isSuperAdmin);
    if (!this.isSuperAdmin) {
      this.modelForm.value.schoolId = this.authenticationService.currentUserValue.schoolId.toString();
    }

    this.loading = true;
    this.teacherService
      .add(this.modelForm.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Teacher created successfully.", {
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
