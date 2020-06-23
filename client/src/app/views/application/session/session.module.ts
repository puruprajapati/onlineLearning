import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { AgGridModule } from "ag-grid-angular";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { AuthGuard } from "../../../helpers";

import { PaginationModule } from "../shared/pagination/pagination.module";
import { UploadModule } from "../shared/upload/upload.module";
import { ConfirmationDialogModule } from "../shared/confirmation-dialog/confirmation-dialog.module";

import { JwtInterceptor, ErrorInterceptor } from "../../../helpers";

import { ListSessionComponent } from "./list-session/list-session.component";
import { AddSessionComponent } from "./add-session/add-session.component";
import { DetailSessionComponent } from "./detail-session/detail-session.component";
import { EditSessionComponent } from "./edit-session/edit-session.component";

@NgModule({
  declarations: [
    ListSessionComponent,
    AddSessionComponent,
    DetailSessionComponent,
    EditSessionComponent,
  ],
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
        component: ListSessionComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Sessions",
        },
      },
      {
        path: "session-edit/:id",
        component: EditSessionComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Edit session",
        },
      },
      {
        path: "session-add",
        component: AddSessionComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Add Session",
        },
      },
      {
        path: "session-detail",
        component: DetailSessionComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Session Detail",
        },
      },
    ]),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class SessionModule {}
