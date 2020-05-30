import { Component, OnInit, ViewChild } from "@angular/core";
import { first } from "rxjs/operators";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { User, Role, List } from "../../../../../models";
import {
  UserService,
  AlertService,
  RoleService,
} from "../../../../../services";
import { MustMatch } from "../../../../../helpers/validator";
@Component({
  selector: "app-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.css"],
})
export class AddUserComponent implements OnInit {
  userForm: FormGroup;
  public submitted: boolean = false;
  public loading: boolean = false;
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
  }

  get f() {
    return this.userForm.controls;
  }

  resetForm() {
    this.userForm = this.formBuilder.group(
      {
        firstName: ["", Validators.required],
        middleName: [""],
        lastName: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        phoneNumber: ["", Validators.required],
        password: ["", [Validators.required, Validators.minLength(6)]],
        confirmPassword: ["", Validators.required],
        parentUser: [""],
        roleId: ["", Validators.required],
      },
      {
        validator: MustMatch("password", "confirmPassword"),
      }
    );
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

    this.loading = true;
    this.userService
      .register(this.userForm.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.alertService.success("Registration successful", {autoClose: true, keepAfterRouteChange: true});
          this.router.navigate(["/settings/user"]);
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
