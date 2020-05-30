import { Component, OnInit } from "@angular/core";
import { first } from "rxjs/operators";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from '@angular/router';
import { User, List } from "../../../../../models";
import { UserService, AlertService, RoleService } from "../../../../../services";
import { MustMatch } from '../../../../../helpers/validator';
@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  userForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
  public selectedUser;
  public roles: List[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private roleService: RoleService
  ) {}

  ngOnInit(): void {
    this.resetForm();
    this.getRoles();
    this.userService.currentSelectedUser.subscribe(user => this.selectedUser = user);
    if(this.selectedUser){
      console.log(this.selectedUser)
      this.userForm.patchValue(this.selectedUser);
    }
  }

  get f() {
    return this.userForm.controls;
  }

  resetForm() {
    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      parentUser: [''],
      roleId: ['', Validators.required],
    });
  }

  getRoles() {
    this.loading = true;
    const roles = localStorage.getItem("roles");
    if (roles) {
      this.roles = JSON.parse(roles);
      this.loading = false;
    } else {
      this.roleService.getAll().subscribe((roles) => {
        this.loading = false;
        this.roles = roles;
      });
    }
  }

  loadParentUser() {

  }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.userForm.invalid) {
      return;
    }

    this.selectedUser.firstName = this.f.firstName.value;
    this.selectedUser.middleName = this.f.middleName.value;
    this.selectedUser.lastName = this.f.lastName.value;
    this.selectedUser.email = this.f.email.value;
    this.selectedUser.phoneNumber = this.f.phoneNumber.value;
    this.selectedUser.parentUser = this.f.parentUser.value;
    this.selectedUser.roleId = this.f.roleId.value;
    delete this.selectedUser.password;

    this.loading = true;
    this.userService
      .udpate(this.selectedUser)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Update successful", {autoClose: true, keepAfterRouteChange: true});
          this.router.navigate(['/settings/user']);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
