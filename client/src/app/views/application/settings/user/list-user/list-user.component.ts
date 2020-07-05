import { Component, OnInit, ViewChild, NgModule, TemplateRef } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { Router } from "@angular/router";

import { User } from "../../../../../models";
import { UserService, AlertService } from "../../../../../services";

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Guid } from 'guid-typescript';

@Component({
  selector: "app-list-user",
  templateUrl: "./list-user.component.html",
  styleUrls: ["./list-user.component.css"],
})
export class ListUserComponent implements OnInit {
  @ViewChild("agGrid") agGrid: AgGridAngular;
  @ViewChild('confirmTemplate') confirmTemplate: any;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: User[];
  public selectedUser;

  public paginationPageSize = 10;
  public totalPages = 3; // default no of pagination navigation button
  public url = "users";
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  modalRef: BsModalRef;
  public selectedId: Guid = null;
  public selectedIds = [];

  constructor(private router: Router, private userService: UserService,
    private modalService: BsModalService,
    private alertService: AlertService) {
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
        headerName: "",
        field: "selectField",
        editable: false,
        checkboxSelection: true,
        width: 50,
      },
      {
        headerName: "User Name",
        field: "userName",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Full Name",
        field: "fullName",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Role",
        field: "userRole",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Email",
        field: "email",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Is Active?",
        field: "active",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Actions",
        width: 250,
        cellRenderer: (params) => {
          return `<a id='delete'><i id='delete' class='fa fa-trash' aria-hidden='true' style='color:#FF0000;' (click)='deleteRow()'></i> Delete</a> &nbsp; &nbsp;
                  <a id='edit'><i id='edit' class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a>`;
        },
      },
    ];
  }

  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;
    this.api.sizeColumnsToFit();
    this.api.setDomLayout("autoHeight");
  }

  onRowClicked(params): void {
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute("id")
    ) {
      this.selectedId = params.data.id;
      if (params.event.srcElement.getAttribute("id") === "delete") {
        this.openConfirmation();
      } else if (params.event.srcElement.getAttribute("id") == "edit") {
        this.editUser(params.data);
      }
    }
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  deleteSelectedRows() {
    this.loading = true;
    const selectRows = this.api.getSelectedRows();
    this.selectedIds = selectRows.map((row) => row.id);
    this.openConfirmation();
  }

  deleteUser() {
    this.loading = true;
    this.userService.delete(this.selectedId).subscribe((user) => {
      this.rowData = this.rowData.filter((u) => u.id !== this.selectedId);
      this.loading = false;
    });
  }

  getUsers() {
    this.loading = true;
    this.userService
      .getAll(this.currentPage + 1, this.paginationPageSize)
      .subscribe((response) => {
        this.loading = false;
        this.rowData = response.body;
        this.paginationData = response.headers.get("X-Pagination");
        this.totalPages = JSON.parse(this.paginationData).TotalPages;
        this.currentPage = JSON.parse(this.paginationData).CurrentPage - 1;
      });
  }

  editUser(user) {
    // used observable to transfer data from list to edit
    this.userService.changeSelectedUser(user);
    this.router.navigate(["/settings/user-edit", user.id]);
  }

  fetchData($event) {
    this.loading = true;
    $event.subscribe((dataSource) => {
      this.loading = false;
      this.rowData = dataSource.body;
      this.paginationData = dataSource.headers.get("X-Pagination");
      this.totalPages = JSON.parse(this.paginationData).TotalPages;
      this.currentPage = JSON.parse(this.paginationData).CurrnetPage;
    });
  }

  openConfirmation() {
    this.modalRef = this.modalService.show(this.confirmTemplate, {class: 'modal-sm'});
  }

  decline(): void {
    this.modalRef.hide();
    this.loading = false;
  }


  confirm(): void {
    if (this.selectedId != null) {
    this.deleteUser();
    } else if (this.selectedIds.length > 0) {
      this.userService.deleteMultiple(this.selectedIds).subscribe((response) => {
        this.rowData = this.rowData.filter(
          (row) => !this.selectedIds.includes(row.id)
        );
      });
      this.alertService.success('Deleted successfully.', {
        autoClose: true,
        keepAfterRouteChange: false,
      });
      this.loading = false;
    }
    this.modalRef.hide();
  }

}
