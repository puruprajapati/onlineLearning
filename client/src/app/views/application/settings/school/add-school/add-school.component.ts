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
import { SchoolService, AlertService } from "../../../../../services";

import { environment } from "../../../../../../environments/environment";

@Component({
  selector: "app-add-school",
  templateUrl: "./add-school.component.html",
  styleUrls: ["./add-school.component.css"],
})
export class AddSchoolComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public uploadResponse = new UploadResponse();
  public imageUrl: string;
  public isUploadComplete: boolean = false;

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
      logoLocation: [""],
      address: [""],
    });
  }

  public uploadFinished = (event) => {
    this.uploadResponse = event;
    this.modelForm.value.logoLocation = this.uploadResponse.dbPath;
    this.isUploadComplete = true;
    this.imageUrl = `${environment.baseUrl}${this.uploadResponse.dbPath}`;
  };

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.modelForm.invalid) {
      return;
    }

    this.loading = true;
    console.log(this.modelForm.value);
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
