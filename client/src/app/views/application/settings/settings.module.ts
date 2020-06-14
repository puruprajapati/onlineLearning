import { RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { AgGridModule } from "ag-grid-angular";

import { AuthGuard } from "../../../helpers";

import { PaginationModule } from "../shared/pagination/pagination.module";
import { UploadModule } from "../shared/upload/upload.module";
import { ConfirmationDialogModule } from "../shared/confirmation-dialog/confirmation-dialog.module";

import { JwtInterceptor, ErrorInterceptor } from "../../../helpers";
import { AddUserComponent } from "./user/add-user/add-user.component";
import { ListUserComponent } from "./user/list-user/list-user.component";
import { EditUserComponent } from "./user/edit-user/edit-user.component";

import { AddSchoolComponent } from "./school/add-school/add-school.component";
import { EditSchoolComponent } from "./school/edit-school/edit-school.component";
import { ListSchoolComponent } from "./school/list-school/list-school.component";
import { AddTeacherComponent } from "./teacher/add-teacher/add-teacher.component";
import { ListTeacherComponent } from "./teacher/list-teacher/list-teacher.component";
import { EditTeacherComponent } from "./teacher/edit-teacher/edit-teacher.component";
import { AddStudentComponent } from "./student/add-student/add-student.component";
import { ListStudentComponent } from "./student/list-student/list-student.component";
import { EditStudentComponent } from "./student/edit-student/edit-student.component";
import { AddClassComponent } from "./class/add-class/add-class.component";
import { ListClassComponent } from "./class/list-class/list-class.component";
import { EditClassComponent } from "./class/edit-class/edit-class.component";

// import { PaginationComponent } from '../shared/pagination/pagination.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    PaginationModule,
    UploadModule,
    ConfirmationDialogModule,
    AgGridModule.withComponents([]),
    RouterModule.forChild([
      {
        path: "",
        data: {
          title: "Settings",
        },
        children: [
          {
            path: "",
            redirectTo: "user",
          },
          {
            path: "user",
            component: ListUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Users",
            },
          },
          {
            path: "user-edit/:id",
            component: EditUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Edit User",
            },
          },
          {
            path: "user-add",
            component: AddUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Add User",
            },
          },
          {
            path: "school",
            component: ListSchoolComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Schools",
            },
          },
          {
            path: "school-edit/:id",
            component: EditSchoolComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Edit School",
            },
          },
          {
            path: "school-add",
            component: AddSchoolComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Add School",
            },
          },
          {
            path: "class",
            component: ListClassComponent,
            canActivate: [AuthGuard],
            data: {
              title: "class",
            },
          },
          {
            path: "class-edit/:id",
            component: EditClassComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Edit Class",
            },
          },
          {
            path: "class-add",
            component: AddClassComponent,
            canActivate: [AuthGuard],
            data: {
              title: "Add Class",
            },
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
  declarations: [
    AddUserComponent,
    ListUserComponent,
    EditUserComponent,
    AddSchoolComponent,
    EditSchoolComponent,
    ListSchoolComponent,
    AddTeacherComponent,
    ListTeacherComponent,
    EditTeacherComponent,
    AddStudentComponent,
    ListStudentComponent,
    EditStudentComponent,
    AddClassComponent,
    ListClassComponent,
    EditClassComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class SettingModule {}
