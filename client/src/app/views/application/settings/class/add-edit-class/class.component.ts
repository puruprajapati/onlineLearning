import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import {
  ClassService,
  AlertService,
} from '../../../../../services';

@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.css']
})
export class ClassComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public selectedClass = null;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private classService: ClassService) { }

  ngOnInit(): void {
    this.resetForm();
    this.classService.currentSelectedClass.subscribe(
      (classDetail) => (this.selectedClass = classDetail)
    );
    if (this.selectedClass) {
      this.modelForm.patchValue(this.selectedClass);
    }
  }

  get f() {
    return this.modelForm.controls;
  }


  resetForm() {
    this.modelForm = this.formBuilder.group({
      className: ['', Validators.required],
      description: ['']
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

  if (this.selectedClass) {

    this.selectedClass.description = this.f.description.value;
    this.selectedClass.className = this.f.className.value;

    this.classService
    .udpate(this.selectedClass)
    .pipe(first())
    .subscribe(
      (data) => {
        this.alertService.success('Class updated successfully.', {
          autoClose: true,
          keepAfterRouteChange: true,
        });
        this.router.navigate(['/settings/class']);
      },
      (error) => {
        this.alertService.error(error);
        this.loading = false;
      }
    );
  } else {
  this.classService
    .add(this.modelForm.value)
    .pipe(first())
    .subscribe(
      (data) => {
        this.alertService.success('Class created successfully.', {
          autoClose: true,
          keepAfterRouteChange: true,
        });
        this.router.navigate(['/settings/class']);
      },
      (error) => {
        this.alertService.error(error);
        this.loading = false;
      }
    );
  } 
}

}
