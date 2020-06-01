import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { ColDef, ColumnApi, GridApi } from "ag-grid-community";
import { PaginationService } from "../../../../services";

@Component({
  selector: "app-pagination",
  templateUrl: "./pagination.component.html",
  styleUrls: ["./pagination.component.scss"],
  providers: [PaginationService],
})
export class PaginationComponent implements OnChanges {
  @Input() pageSize = 0;
  @Input() gridApi: GridApi;
  @Input() noOfPages = 0;
  private paginationPages = {};

  get PaginationPages() {
    return this.paginationPages;
  }

  get currentPage(): number {
    return this.gridApi ? this.gridApi.paginationGetCurrentPage() : 0;
  }

  get totalPages(): number {
    return this.noOfPages;
  }

  constructor(private paginationService: PaginationService) {}

  ngOnChanges(changes: SimpleChanges) {
    for (const propName in changes) {
      if (propName === "noOfPages") {
        this.paginationPages = this.noOfPages
          ? this.paginationService.getPager(this.noOfPages, 1)
          : {};
      }
    }
  }

  goToPage(index: number) {
    this.gridApi.paginationGoToPage(index);
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      index + 1
    );
  }

  goToNext() {
    this.gridApi.paginationGoToNextPage();
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      this.currentPage
    );
  }

  goToPrevious() {
    this.gridApi.paginationGoToPreviousPage();
    this.paginationPages = this.paginationService.getPager(
      this.noOfPages,
      this.currentPage
    );
  }
}
