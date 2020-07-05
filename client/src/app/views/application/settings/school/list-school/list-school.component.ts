import { Component, OnInit, ViewChild, NgModule, TemplateRef } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { School } from '../../../../../models';
import {
  SchoolService,
  ConfirmationDialogService,
  AlertService,
} from '../../../../../services';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-list-school',
  templateUrl: './list-school.component.html',
  styleUrls: ['./list-school.component.css'],
})
export class ListSchoolComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridAngular;
  @ViewChild('confirmTemplate') confirmTemplate: any;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: School[];
  public selectedSchool;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = 'school';
  public currentPage = 0; // default page no to load data
  public paginationData: any;
  modalRef: BsModalRef;
  public selectedId: Guid = null;
  public selectedIds = [];

  constructor(
    private router: Router,
    private schoolService: SchoolService,
    private confirmationDialogService: ConfirmationDialogService,
    private modalService: BsModalService,
    private alertService: AlertService
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
        headerName: '',
        field: 'selectField',
        editable: false,
        checkboxSelection: true,
        width: 50,
      },
      {
        headerName: 'School Name',
        field: 'name',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Address',
        field: 'address',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Contact Number',
        field: 'contactNumber',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Email',
        field: 'emailAddress',
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
    this.api.sizeColumnsToFit();
    this.api.setDomLayout('autoHeight');
  }

  onRowClicked(params): void {
    if (
      params.event.srcElement !== undefined &&
      params.event.srcElement.getAttribute('id')
    ) {
      this.selectedId = params.data.id;
      if (params.event.srcElement.getAttribute('id') === 'delete') {
        this.openConfirmation();
      } else if (params.event.srcElement.getAttribute('id') === 'edit') {
        // TODO: edit link is not working for icon
        this.editModel(params.data);
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
    this.selectedIds = selectRows.map((row) => row.id);
  }

  deleteModel() {
    console.log('selected school id', this.selectedId);
    this.alertService.clear();
    this.loading = true;
    this.schoolService.delete(this.selectedId).subscribe((school) => {
      this.rowData = this.rowData.filter((u) => u.id !== this.selectedId);
      this.loading = false;
      this.alertService.success('Deleted successfully.', {
        autoClose: true,
        keepAfterRouteChange: false,
      });
      this.selectedId = null;
    });
  }

  getSchools() {
    this.loading = true;
    this.schoolService
      .getAll(this.currentPage + 1, this.paginationPageSize)
      .subscribe((response) => {
        this.loading = false;
        this.rowData = response.body;
        this.paginationData = response.headers.get('X-Pagination');
        if (this.paginationData) {
          this.totalPages = JSON.parse(this.paginationData).TotalPages;
          this.currentPage = JSON.parse(this.paginationData).CurrentPage - 1;
        }
      });
  }

  editModel(school) {
    // used observable to transfer data from list to edit
    this.schoolService.changeSelectedSchool(school);
    this.router.navigate(['/settings/school-edit', school.id]);
  }

  fetchData($event) {
    this.loading = true;
    $event.subscribe((dataSource) => {
      this.loading = false;
      this.rowData = dataSource.body;
      this.paginationData = dataSource.headers.get('X-Pagination');
      this.totalPages = JSON.parse(this.paginationData).TotalPages;
      this.currentPage = JSON.parse(this.paginationData).CurrnetPage;
    });
  }

  openConfirmation() {
    this.modalRef = this.modalService.show(this.confirmTemplate, {class: 'modal-sm'});
  }

  confirm(): void {
    if (this.selectedId !== null) {
      console.log('selected id', this.selectedId);
      this.deleteModel();
    } else if (this.selectedIds.length > 0) {
      this.schoolService.deleteMultiple(this.selectedIds).subscribe((response) => {
        this.rowData = this.rowData.filter(
          (row) => !this.selectedIds.includes(row.id)
        );
        this.loading = false;
        this.alertService.success('Deleted successfully.', {
          autoClose: true,
          keepAfterRouteChange: false,
        });
        this.selectedIds = [];
      });
    }
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

}
