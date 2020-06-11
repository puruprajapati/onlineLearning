import { Component, Input, OnInit } from "@angular/core";
import { BsModalRef, ModalModule } from "ngx-bootstrap/modal";
import { Subject } from "rxjs";

@Component({
  selector: "app-confirmation-dialog",
  templateUrl: "./confirmation-dialog.component.html",
  styleUrls: ["./confirmation-dialog.component.css"],
})
export class ConfirmationDialogComponent implements OnInit {
  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnCancelText: string;

  public onClose: Subject<boolean>;

  constructor(private bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.onClose = new Subject();
  }

  public decline() {
    this.onClose.next(false);
    this.bsModalRef.hide();
  }

  public accept() {
    this.onClose.next(true);
    this.bsModalRef.hide();
  }

  public dismiss() {
    this.bsModalRef.hide();
  }
}
