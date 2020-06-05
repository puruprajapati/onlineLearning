import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
  NgModule,
} from "@angular/core";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { PaginationService } from "../../../../services";

import { environment } from "../../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { tap, catchError, map } from "rxjs/operators";
import { BehaviorSubject, Observable } from "rxjs";
// import { EventEmitter } from 'protractor';

@Component({
  selector: "app-pagination",
  templateUrl: "./pagination.component.html",
  styleUrls: ["./pagination.component.scss"],
  providers: [PaginationService],
})
export class PaginationComponent implements OnChanges {
  @Input() pageSize = 0;
  @Input() noOfPages = 1;
  @Input() currentPage = 0;
  @Input() url = "";

  public rows: any;

  @Output() fetchDataEvent = new EventEmitter<any>();
  public paginationPages: any;

  get PaginationPages() {
    return this.paginationPages;
  }

  get totalPages(): number {
    return this.noOfPages;
  }

  constructor(
    private paginationService: PaginationService,
    private http: HttpClient
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    for (const propName in changes) {
      if (propName === "noOfPages") {
        this.paginationPages = this.noOfPages
          ? this.paginationService.getPager(this.noOfPages, 1)
          : {};
      }
    }
    this.currentPage = 0; // assuming first time it will always load page 1
  }

  goToPage(index: number) {
    this.currentPage = index;
    this.rows = this.getAll(index);
    this.fetchDataEvent.emit(this.rows);
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      index + 1
    );
  }

  goToNext() {
    this.currentPage = this.currentPage + 1;
    this.rows = this.getAll(this.currentPage);
    this.fetchDataEvent.emit(this.rows);
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      this.currentPage
    );
  }

  goToPrevious() {
    this.currentPage = this.currentPage - 1;
    this.rows = this.getAll(this.currentPage);
    this.fetchDataEvent.emit(this.rows);
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      this.currentPage
    );
  }

  getAll(currPage) {
    const pageToNavigate = currPage + 1;
    return this.http
      .get<any[]>(
        `${environment.apiUrl}/${this.url}?PageNumber=${pageToNavigate}&PageSize=${this.pageSize}`,
        { observe: "response" }
      )
      .pipe(
        map((response) => {
          return response;
        })
      );
  }
}
