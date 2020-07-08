import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormGroupName,
} from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import {
  ClassService,
  AlertService,
  AuthenticationService,
} from '../../../../../services';

@Component({
  selector: 'app-add-class',
  templateUrl: './add-class.component.html',
  styleUrls: ['./add-class.component.css']
})
export class AddClassComponent implements OnInit {
  modelForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private classService: ClassService) { }

  ngOnInit(): void {
    this.resetForm();
    const currentUser = this.authenticationService.currentUserValue;
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
