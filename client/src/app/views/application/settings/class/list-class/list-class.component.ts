import { Component, OnInit, ViewChild, NgModule, TemplateRef } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { Router } from '@angular/router';

import { Class, User } from '../../../../../models';
import { EnumRole } from '../../../../../enums';
import {
  ClassService,
  AuthenticationService,
  AlertService,
  ConfirmationDialogService,
} from '../../../../../services';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-list-class',
  templateUrl: './list-class.component.html',
  styleUrls: ['./list-class.component.css'],
})
export class ListClassComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridAngular;
  @ViewChild('confirmTemplate') confirmTemplate: any;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: Class[];
  public selectedClass;
  public isSuperAdmin: boolean = false;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = 'class';
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  modalRef: BsModalRef;
  public selectedId: Guid = null;
  public selectedIds = [];

  constructor(
    private router: Router,
    private classSerice: ClassService,
    private confirmationDialogService: ConfirmationDialogService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private modalService: BsModalService,
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    const currentUser = this.authenticationService.currentUserValue;
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
        headerName: '',
        field: 'selectField',
        editable: false,
        checkboxSelection: true,
        width: 50,
      },
      {
        headerName: 'Class Name',
        field: 'className',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Description',
        field: 'description',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Actions',
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
    this.openConfirmation();
  }

  deleteModel() {
    this.alertService.clear();
    this.loading = true;

    this.classSerice.delete(this.selectedId).subscribe((modelData) => {
      this.rowData = this.rowData.filter((u) => u.id !== this.selectedId);
      this.loading = false;
      this.alertService.success('Deleted successfully.', {
        autoClose: true,
        keepAfterRouteChange: false,
      });
      this.selectedId = null;
    });
  }

  getClass() {
    this.loading = true;
    this.classSerice
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

  editModel(modelData) {
    // used observable to transfer data from list to edit
    this.classSerice.changeSelectedClass(modelData);
    this.router.navigate(['/settings/class-edit', modelData.id]);
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

  decline(): void {
    this.loading = false;
    this.modalRef.hide();
  }


  confirm(): void {
    if (this.selectedId != null) {
    this.deleteModel();
    } else if (this.selectedIds.length > 0) {
      this.classSerice.deleteMultiple(this.selectedIds).subscribe((response) => {
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

}
