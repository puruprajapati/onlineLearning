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
import {
  SchoolService,
  AlertService,
  RoleService,
} from "../../../../../services";
import { STATUS } from "../../../../../enums";

@Component({
  selector: "app-edit-school",
  templateUrl: "./edit-school.component.html",
  styleUrls: ["./edit-school.component.css"],
})
export class EditSchoolComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;

  public selectedSchool;
  public statusList = Object.values(STATUS);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private schoolService: SchoolService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.resetForm();
    this.schoolService.currentSelectedSchool.subscribe(
      (school) => (this.selectedSchool = school)
    );
    if (this.selectedSchool) {
      this.modelForm.patchValue(this.selectedSchool);
    }
  }

  get f() {
    return this.modelForm.controls;
  }

  resetForm() {
    this.modelForm = this.formBuilder.group({
      schoolCode: ["", Validators.required],
      name: ["", Validators.required],
      emailAddress: ["", [Validators.required, Validators.email]],
      contactNumber: ["", Validators.required],
      logoLocation: [""],
      address: [""],
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

    this.selectedSchool.schoolCode = this.f.schoolCode.value;
    this.selectedSchool.name = this.f.name.value;
    this.selectedSchool.emailAddress = this.f.emailAddress.value;
    this.selectedSchool.contactNumber = this.f.contactNumber.value;
    this.selectedSchool.logoLocation = this.f.logoLocation.value;
    this.selectedSchool.address = this.f.address.value;
    this.selectedSchool.active = this.f.active.value;

    this.loading = true;
    this.schoolService
      .udpate(this.selectedSchool)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Update successful.", {
            autoClose: true,
            keepAfterRouteChange: true,
          });
          this.router.navigate(["/settings/school"]);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
