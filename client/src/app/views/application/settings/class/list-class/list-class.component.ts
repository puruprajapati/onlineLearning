import { Component, OnInit, ViewChild, NgModule } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { Router } from "@angular/router";

import { Class, User } from "../../../../../models";
import { EnumRole } from "../../../../../enums";
import {
  ClassService,
  AuthenticationService,
  AlertService,
  ConfirmationDialogService,
} from "../../../../../services";

@Component({
  selector: "app-list-class",
  templateUrl: "./list-class.component.html",
  styleUrls: ["./list-class.component.css"],
})
export class ListClassComponent implements OnInit {
  @ViewChild("agGrid") agGrid: AgGridAngular;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: Class[];
  public selectedClass;
  public isSuperAdmin: boolean = false;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = "class";
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  constructor(
    private router: Router,
    private classSerice: ClassService,
    private confirmationDialogService: ConfirmationDialogService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    let currentUser = this.authenticationService.currentUserValue;
    if (currentUser.userRole === EnumRole.SuperAdmin.toString()) {
      this.isSuperAdmin = true;
    }
    this.getClass();
    this.classSerice.currentSelectedClass.subscribe(
      (classData) => (this.selectedClass = classData)
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
        headerName: "School Name",
        field: "schoolName",
        editable: false,
        sortable: true,
        filter: true,
        hide: this.isSuperAdmin,
      },
      {
        headerName: "Class Name",
        field: "className",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Description",
        field: "description",
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
    let classId = 0;
    let action = "";
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute("id")
    ) {
      classId = params.data.id;
      if (params.event.srcElement.getAttribute("id") === "delete") {
        action = "delete";
      } else if (params.event.srcElement.getAttribute("id") == "edit") {
        action = "edit";
      }
    }
    if (classId) {
      if (action === "edit") {
        this.editModel(params.data);
      } else if (action === "delete") {
        this.deleteModel(classId);
      }
    }
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  deleteSelectedRows() {
    this.alertService.clear();
    this.loading = true;
    const selectRows = this.api.getSelectedRows();
    const selectedIds = selectRows.map((row) => row.id);
    this.classSerice.deleteMultiple(selectedIds).subscribe((response) => {
      this.rowData = this.rowData.filter(
        (row) => !selectedIds.includes(row.id)
      );
      this.loading = false;
      this.alertService.success("Deleted successfully.", {
        autoClose: true,
        keepAfterRouteChange: false,
      });
    });
  }

  deleteModel(modelId) {
    this.alertService.clear();
    this.loading = true;

    this.classSerice.delete(modelId).subscribe((modelData) => {
      this.rowData = this.rowData.filter((u) => u.id !== modelId);
      this.loading = false;
      this.alertService.success("Deleted successfully.", {
        autoClose: true,
        keepAfterRouteChange: false,
      });
    });
  }

  getClass() {
    this.loading = true;
    this.classSerice
      .getAll(this.currentPage + 1, this.paginationPageSize)
      .subscribe((response) => {
        this.loading = false;
        this.rowData = response.body;
        this.paginationData = response.headers.get("X-Pagination");
        if (this.paginationData) {
          this.totalPages = JSON.parse(this.paginationData).TotalPages;
          this.currentPage = JSON.parse(this.paginationData).CurrentPage - 1;
        }
      });
  }

  editModel(modelData) {
    // used observable to transfer data from list to edit
    this.classSerice.changeSelectedClass(modelData);
    this.router.navigate(["/settings/class-edit", modelData.id]);
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
}
