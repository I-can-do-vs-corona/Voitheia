import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgbPopoverModule, NgbAlertModule, NgbCollapseModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbAlertModule,
    NgbPopoverModule,
    NgbCollapseModule,
    NgbDropdownModule
  ],
  exports: [
    NgbAlertModule,
    NgbPopoverModule,
    NgbCollapseModule,
    NgbDropdownModule
  ]
})
export class CustomBootstrapModule { }
