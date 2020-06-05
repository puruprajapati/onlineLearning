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

@Component({
  selector: "app-add-school",
  templateUrl: "./add-school.component.html",
  styleUrls: ["./add-school.component.css"],
})
export class AddSchoolComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private schoolService: SchoolService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.resetForm();
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
      logoLocatoin: [""],
      address: [""],
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

    this.loading = true;
    this.schoolService
      .add(this.modelForm.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("School added successfully.", {
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
