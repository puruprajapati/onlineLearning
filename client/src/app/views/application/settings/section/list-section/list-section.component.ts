import { Component, OnInit, ViewChild, NgModule } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { Router } from "@angular/router";

import { Section } from "../../../../../models";
import {
  SectionService,
  ConfirmationDialogService,
  AlertService,
} from "../../../../../services";

@Component({
  selector: "app-list-section",
  templateUrl: "./list-section.component.html",
  styleUrls: ["./list-section.component.css"],
})
export class ListSectionComponent implements OnInit {
  @ViewChild("agGrid") agGrid: AgGridAngular;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: Section[];
  public selectedSchool;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = "section";
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  constructor(
    private router: Router,
    private sectionService: SectionService,
    private confirmationDialogService: ConfirmationDialogService,

    private alertService: AlertService
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    this.getSections();
    this.sectionService.currentSelectedModel.subscribe(
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
        headerName: "Section Name",
        field: "sectionName",
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
                  <a id='edit'><i id='delete' class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a>`;
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
    let modelId = 0;
    let action = "";
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute("id")
    ) {
      modelId = params.data.id;
      if (params.event.srcElement.getAttribute("id") === "delete") {
        action = "delete";
      } else if (params.event.srcElement.getAttribute("id") == "edit") {
        action = "edit";
      }
    }
    if (modelId) {
      if (action === "edit") {
        this.editModel(params.data);
      } else if (action === "delete") {
        this.deleteModel(modelId);
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
    // this.confirmationDialogService
    //   .openConfirmDialog("Confirm", "Are you sure want to delete?")
    //   .subscribe((result) => {
    //     console.log(result, "testssss");
    //   });
    this.sectionService.deleteMultiple(selectedIds).subscribe((response) => {
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

    // this.confirmationDialogService
    //   .openConfirmDialog("Confirm", "Are you sure want to delete?")
    //   .subscribe((result) => {
    //     console.log(result, "testssss");
    //   });
    this.sectionService.delete(modelId).subscribe((school) => {
      this.rowData = this.rowData.filter((u) => u.id !== modelId);
      this.loading = false;
      this.alertService.success("Deleted successfully.", {
        autoClose: true,
        keepAfterRouteChange: false,
      });
    });
  }

  getSections() {
    this.loading = true;
    this.sectionService
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
    this.sectionService.changeSelectedModel(school);
    this.router.navigate(["/settings/section-edit", school.id]);
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
