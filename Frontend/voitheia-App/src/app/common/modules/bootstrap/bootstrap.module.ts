import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgbPopoverModule, NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbAlertModule,
    NgbPopoverModule
  ],
  exports: [
    NgbAlertModule,
    NgbPopoverModule
  ]
})
export class CustomBootstrapModule { }
