import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ModalModule } from "ngx-bootstrap/modal";

import { ConfirmationDialogComponent } from "./confirmation-dialog.component";

@NgModule({
  imports: [CommonModule, ModalModule.forRoot()],
  declarations: [ConfirmationDialogComponent],
  exports: [ConfirmationDialogComponent],
})
export class ConfirmationDialogModule {}
