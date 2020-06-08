import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";

import { ConfirmationDialogModule } from "../../views/application/shared/confirmation-dialog/confirmation-dialog.module";

@Injectable({ providedIn: "root" })
export class ConfirmationDialogService {
  public modalRef: BsModalRef;

  constructor(private modalService: BsModalService) {}

  // public openConfirmDialog(
  //   title: string,
  //   message: string,
  //   btnOkText: string = 'OK',
  //   btnCancelText: string = 'Cancel',
  //   dialogSize: 'sm'|'lg' = 'sm'): Promise<boolean> {
  //   const modalRef = this.modalService.show(ConfirmationDialogComponent, { size: dialogSize });
  //   modalRef.componentInstance.title = title;
  //   modalRef.componentInstance.message = message;
  //   modalRef.componentInstance.btnOkText = btnOkText;
  //   modalRef.componentInstance.btnCancelText = btnCancelText;

  //   return modalRef.result;
  // }

  public openConfirmDialog(
    title: string,
    message: string,
    btnOkText: string = "OK",
    btnCancelText: string = "Cancel"
  ) {
    const initialState = {
      title,
      content: message,
    };
    this.modalRef = this.modalService.show(ConfirmationDialogModule, {
      initialState,
    });
    this.modalRef.content.closeBtnName = btnCancelText;
    return this.modalRef.content.onClose((result) => result);
  }
}
