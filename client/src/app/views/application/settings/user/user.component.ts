import { User } from '../../../../models';
import { UserService, AlertService } from '../../../../services';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { Observable } from 'rxjs';
import { AgGridAngular } from 'ag-grid-angular';
import { MODE } from '../../../../enums';
import { first } from 'rxjs/operators';


@Component({
  selector: 'users',
  templateUrl: './user.component.html',
})
export class UserComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridAngular;

  userForm: FormGroup;
  public loading: boolean = false;
  public submitted: boolean = false;

  public rowData: User[];
  private columnDefs: ColDef[];
  // gridApi and columnApi
  private api: GridApi;
  private columnApi: ColumnApi;

  public mode;
  public addUrl;
  public listUrl;
  public title: string;

  // rowData: Observable<User[]>;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit() {
    this.title = 'khatrattiel';
    this.mode = MODE.LIST;
    this.loading = true;
    this.userService.getAll().subscribe((users) => {
      this.loading = false;
      this.rowData = users;
    });
    // this.rowData = this.userService.getAll();

    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: [''],
      password: ['', [Validators.required, Validators.minLength(6)]],
      parentUser: [''],
      role: ['']
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.userForm.controls;
  }

  changeMode(mode): void{
    this.mode = mode;
    this.submitted = false;
  }

  getSelectedRows() {
    const selectedNodes = this.agGrid.api.getSelectedNodes();
    const selectedData = selectedNodes.map((node) => node.data);
    const selectedDataStringPresentation = selectedData
      .map((node) => node.id)
      .join(', ');
    alert(`Selected nodes: ${selectedDataStringPresentation}`);
  }

  // one grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;

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
      if(params.event.srcElement.getAttribute('id') === 'delete'){
        action = 'delete';
      } else if (params.event.srcElement.getAttribute('id') == 'edit') {
        action = 'edit';
      }
    }
    if(userId) {
      console.log(action);
      console.log(params.data)
    }

  }

  onSubmit(){
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.userForm.invalid) {
        return;
    }

    this.loading = true;
    this.userService.register(this.userForm.value)
        .pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Registration successful', true);
            },
            error => {
                this.alertService.error(error);
                this.loading = false;
            });
  }

  rowsSelected(){
    return this.api && this.api.getSelectedRows().length > 0;
  }

  deleteSelectedRows() {
    const selectRows = this.api.getSelectedRows();
    console.log(selectRows);
    // create an Observable for each row to delete
    // const deleteSubscriptions = selectRows.map((rowToDelete) => {
    //     return this.athleteService.delete(rowToDelete);
    // });
    // then subscribe to these and once all done, refresh the grid data
  //     Observable.forkJoin(...deleteSubscriptions)
  //               .subscribe(results => this.setAthleteRowData())
  }


  // create some simple column definitions
  private createColumnDefs() {
    return [
        {headerName: '',                    field: 'selectField'     , editable: false, checkboxSelection: true, width: 50 },
        {headerName: 'First Name',          field: 'firstName'       , editable: false, sortable: true, filter: true},
        {headerName: 'Middle Name',         field: 'middleName'      , editable: false, sortable: true, filter: true},
        {headerName: 'Last Name',           field: 'lastName'        , editable: false, sortable: true, filter: true},
        {headerName: 'Email',               field: 'email'           , editable: false, sortable: true, filter: true},
        {headerName: 'Is Email Confirmed?', field: 'isEmailConfirmed', editable: false, sortable: true, filter: true},
        {headerName: 'Is Active?',          field: 'active'          , editable: false, sortable: true, filter: true},
        {headerName: 'Actions', width: 200, cellRenderer: (params)=>{
          return `<a id='delete'><i class='fa fa-trash' aria-hidden='true' style='color:#FF0000;' (click)='deleteRow()'></i> Delete</a> &nbsp; &nbsp;
                  <a id='edit'><i class='fa fa-edit' aria-hidden='true' style='color:#228B22;' (click)='editRow()'></i>&nbsp; Edit</a>`
        }}
      ];
  }
}


// { headerName: 'Actions', width: 125,
//         cellRenderer: (data) => {
//             // here, I was hoping the 'data' would refer to the entire
//             // row / object being bound, so that I could check for
//             // certain conditions to be true (or false)
//             if (data.IsGlobal.value === true)
//             {
//                 return '<span class='far fa-trash-alt mr-2' title='Delete entry'></span>' +
//                        '<span class='fab fa-nintendo-switch' title='Promote entry'></span>';
//             }
//             else
//             {
//                 return '<span class='far fa-trash-alt mr-2' title='Delete'></span>';
//             }
//         }
//     }

// {field: 'country', valueGetter: (params) => params.data.country.name},{field: 'country', valueGetter: (params) => params.data.country.name},
