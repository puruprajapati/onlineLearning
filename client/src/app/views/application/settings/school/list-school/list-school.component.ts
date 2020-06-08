import { Component, OnInit, ViewChild, NgModule } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { Router } from "@angular/router";

import { School } from "../../../../../models";
import {
  SchoolService,
  ConfirmationDialogService,
} from "../../../../../services";

@Component({
  selector: "app-list-school",
  templateUrl: "./list-school.component.html",
  styleUrls: ["./list-school.component.css"],
})
export class ListSchoolComponent implements OnInit {
  @ViewChild("agGrid") agGrid: AgGridAngular;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: School[];
  public selectedSchool;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = "school";
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  constructor(
    private router: Router,
    private schoolService: SchoolService,
    private confirmationDialogService: ConfirmationDialogService
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    this.getSchools();
    this.schoolService.currentSelectedSchool.subscribe(
      (school) => (this.selectedSchool = school)
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
        field: "name",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Address",
        field: "address",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Contact Number",
        field: "contactNumber",
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: "Email",
        field: "emailAddress",
        editable: false,
        sortable: true,
        filter: true,
      },
      // {
      //   headerName: "Logo",
      //   field: "logoLocation",
      //   editable: false,
      //   sortable: true,
      //   filter: true,
      // },
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
          return `<a id='delete'><i class='fa fa-trash' aria-hidden='true' style='color:#FF0000;' (click)='deleteRow()'></i> Delete</a> &nbsp; &nbsp;
                  <a id='edit'><i class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a>`;
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
    let schoolId = 0;
    let action = "";
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute("id")
    ) {
      schoolId = params.data.id;
      if (params.event.srcElement.getAttribute("id") === "delete") {
        action = "delete";
      } else if (params.event.srcElement.getAttribute("id") == "edit") {
        action = "edit";
      }
    }
    //TODO: edit link is not working for icon
    console.log("check", schoolId, action);
    if (schoolId) {
      if (action === "edit") {
        this.editModel(params.data);
      } else if (action === "delete") {
        this.deleteModel(schoolId);
      }
    }
  }

  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }

  deleteSelectedRows() {
    this.loading = true;
    const selectRows = this.api.getSelectedRows();
    const selectedIds = selectRows.map((row) => row.id);
    this.confirmationDialogService
      .openConfirmDialog("Confirm", "Are you sure want to delete?")
      .subscribe((result) => {
        console.log(result, "testssss");
      });
    // this.schoolService.deleteMultiple(selectedIds).subscribe((response) => {
    //   console.log(response);
    //   this.rowData = this.rowData.filter(
    //     (row) => !selectedIds.includes(row.id)
    //   );
    //   this.loading = false;
    // });
  }

  deleteModel(schoolId) {
    this.loading = true;

    this.confirmationDialogService
      .openConfirmDialog("Confirm", "Are you sure want to delete?")
      .subscribe((result) => {
        console.log(result, "testssss");
      });
    this.schoolService.delete(schoolId).subscribe((school) => {
      this.rowData = this.rowData.filter((u) => u.id !== schoolId);
      this.loading = false;
    });
  }

  getSchools() {
    this.loading = true;
    this.schoolService
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

  editModel(school) {
    // used observable to transfer data from list to edit
    this.schoolService.changeSelectedSchool(school);
    this.router.navigate(["/settings/school-edit", school.id]);
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
