import { Component, OnInit, ViewChild, NgModule } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { Router } from '@angular/router';

import { User } from '../../../../../models';
import { UserService } from '../../../../../services';


@Component({
  selector: 'app-list-user',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.css'],
})


export class ListUserComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridAngular;

  public loading: boolean = false;
  public rowData: User[];
  public selectedUser;
  public paginationData;

  private columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  private paginationPageSize = 10;
  private totalPages = 0;
  private pageNumber = 1;

  get PaginationPageSize(): number {
    return this.paginationPageSize;
  }

  get gridAPI(): GridApi {
    return this.api;
  }

  get TotalPages(): number {
    return this.totalPages;
  }

  constructor(private router: Router, private userService: UserService) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    this.getUsers();
    this.userService.currentSelectedUser.subscribe(
      (user) => (this.selectedUser = user)
    );
  }

  private createColumnDefs() {
    return [
      {
        headerName: '',
        field: 'selectField',
        editable: false,
        checkboxSelection: true,
        width: 50,
      },
      {
        headerName: 'User Name',
        field: 'userName',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Full Name',
        field: 'fullName',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Role',
        field: 'userRole',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Email',
        field: 'email',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Is Active?',
        field: 'active',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Actions',
        width: 250,
        cellRenderer: (params) => {
          return `<a id='delete'><i class='fa fa-trash' aria-hidden='true' style='color:#FF0000;' (click)='deleteRow()'></i> Delete</a> &nbsp; &nbsp;
                  <a id='edit'><i class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a>`;
        },
      },
    ];
  }

  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;
    // this.totalPages = this.paginationData.TotalCount;

    this.api.sizeColumnsToFit();
  }

  onRowClicked(params): void {
    let userId = 0;
    let action = '';
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute('id')
    ) {
      userId = params.data.id;
      if (params.event.srcElement.getAttribute('id') === 'delete') {
        action = 'delete';
      } else if (params.event.srcElement.getAttribute('id') == 'edit') {
        action = 'edit';
      }
    }
    //TODO: edit link is not working for icon
    console.log('check', userId, action);
    if (userId) {
      if (action === 'edit') {
        this.editUser(params.data);
      } else if (action === 'delete') {
        this.deleteUser(userId);
      }
    }
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  deleteSelectedRows() {
    const selectRows = this.api.getSelectedRows();
    //TODO: delete multiple users at a time
    console.log(selectRows);
  }

  deleteUser(userId) {
    //TODO: confirmation box to delete and toastr
    this.loading = true;
    this.userService.delete(userId).subscribe((user) => {
      this.rowData = this.rowData.filter((u) => u.id !== userId);
      this.loading = false;
    });
  }

  getUsers() {
    this.loading = true;
    this.userService
      .getAll(this.pageNumber, this.paginationPageSize)
      .subscribe((response) => {
        this.loading = false;
        this.rowData = response.body;
        this.paginationData = response.headers.get('X-Pagination');
        this.totalPages = this.paginationData.TotalCount;
        console.log(this.paginationData);
      });
  }

  editUser(user) {
    // used observable to transfer data from list to edit
    this.userService.changeSelectedUser(user);
    this.router.navigate(['/settings/user-edit', user.id]);
  }
}

//[domLayout]="'autoHeight'" in html
