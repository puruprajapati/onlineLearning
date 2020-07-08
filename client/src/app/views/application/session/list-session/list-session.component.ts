import { Component, OnInit, ViewChild, NgModule, TemplateRef, LOCALE_ID, Inject } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { Router } from '@angular/router';

import { Session, List } from '../../../../models';
import { EnumRole } from '../../../../enums';
import {
  SessionService,
  AuthenticationService,
  AlertService,
  ConfirmationDialogService,
  ListService,
} from '../../../../services';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Guid } from 'guid-typescript';
import * as moment from 'moment';

@Component({
  selector: 'app-list-session',
  templateUrl: './list-session.component.html',
  styleUrls: ['./list-session.component.css'],
})
export class ListSessionComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridAngular;
  @ViewChild('confirmTemplate') confirmTemplate: any;

  public columnDefs: ColDef[];
  private api: GridApi;
  private columnApi: ColumnApi;

  public loading: boolean = false;
  public rowData: Session[];
  public allList: any;
  public classList: List[];
  public sectionList: List[];
  public selectedModel;
  public isSuperAdmin: boolean = false;

  public paginationPageSize = 10;
  public totalPages = 1; // default no of pagination navigation button
  public url = 'session';
  public currentPage = 0; // default page no to load data
  public paginationData: any;

  modalRef: BsModalRef;
  public selectedId: Guid = null;
  public selectedIds = [];

  constructor(
    private router: Router,
    private sessionService: SessionService,
    private confirmationDialogService: ConfirmationDialogService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private listService: ListService,
    private modalService: BsModalService,
    @Inject(LOCALE_ID) private locale: string
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit(): void {
    const currentUser = this.authenticationService.currentUserValue;
    if (currentUser.userRole === EnumRole.SuperAdmin.toString()) {
      this.isSuperAdmin = true;
    }
    this.getSession();
    this.getList();
    this.sessionService.currentSelectedModel.subscribe(
      (modelData) => (this.selectedModel = modelData)
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
        headerName: 'Session Title',
        field: 'sessionTitle',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Description',
        field: 'sessionDesc',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Class',
        field: 'className',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Teacher',
        field: 'teacherName',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Scheduled Date',
        field: 'scheduledDate',
        valueFormatter: this.dateFormatter,
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Start Time',
        field: 'startingTime',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'End Time',
        field: 'endingTime',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Status',
        field: 'sessionStatus',
        editable: false,
        sortable: true,
        filter: true,
      },
      {
        headerName: 'Actions',
        width: 400,
        cellRenderer: (params) => {
          return `<a id='delete'><i id='delete' class='fa fa-trash' aria-hidden='true' style='color:#FF0000;' (click)='deleteRow()'></i> Delete</a> &nbsp; &nbsp;
                  <a id='edit'><i id='edit' class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a> &nbsp; &nbsp;
                  <a id='detail'><i id='detail' class='fa fa-th' aria-hidden='true' style='color:#1B85CE;' (click)='editRow()'></i>&nbsp; Detail</a>`;
        },
      },
    ];
  }

  dateFormatter(params) {
    return moment(params.value).format('MM/DD/YYYY');
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
      } else if (params.event.srcElement.getAttribute('id') === 'detail') {
        // this.detailModel(modelId);
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

    this.sessionService.delete(this.selectedId).subscribe((modelData) => {
      this.rowData = this.rowData.filter((u) => u.id !== this.selectedId);
      this.loading = false;
      this.alertService.success('Deleted successfully.', {
        autoClose: true,
        keepAfterRouteChange: false,
      });
      this.selectedId = null;
    });
  }

  getList() {
    this.loading = true;
    this.listService.getAll().subscribe((response) => {
      this.loading = false;
      this.allList = response;
      this.classList = this.allList[1];
      this.sectionList = this.allList[2];
    });
  }

  getSession() {
    this.loading = true;
    this.sessionService
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
    this.sessionService.changeSelectedModel(modelData);
    this.router.navigate(['/settings/session-edit', modelData.id]);
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
      this.sessionService.deleteMultiple(this.selectedIds).subscribe((response) => {
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
