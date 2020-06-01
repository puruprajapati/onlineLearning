import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AgGridModule } from 'ag-grid-angular';

import { AuthGuard } from '../../../helpers';

import { JwtInterceptor, ErrorInterceptor } from '../../../helpers';
import { AddUserComponent } from './user/add-user/add-user.component';
import { ListUserComponent } from './user/list-user/list-user.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';

 import { PaginationModule } from '../shared/pagination/pagination.module';
// import { PaginationComponent } from '../shared/pagination/pagination.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    PaginationModule,
    AgGridModule.withComponents([]),
    RouterModule.forChild([
      {
        path: '',
        data: {
          title: 'Settings',
        },
        children: [
          {
            path: '',
            redirectTo: 'user',
          },
          {
            path: 'user',
            component: ListUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: 'Users',
            },
          },
          {
            path: 'user-edit/:id',
            component: EditUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: 'Edit User',
            },
          },
          {
            path: 'user-add',
            component: AddUserComponent,
            canActivate: [AuthGuard],
            data: {
              title: 'Add User',
            },
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
  declarations: [AddUserComponent, ListUserComponent, EditUserComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class SettingModule {}
