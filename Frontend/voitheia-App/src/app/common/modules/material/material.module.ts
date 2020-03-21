import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatInputModule} from '@angular/material/input';
import {MatStepperModule} from '@angular/material/stepper';
import {MatRadioModule} from '@angular/material/radio';
import {MatTableModule } from '@angular/material/table';
import {MatSelectModule} from '@angular/material/select';
import {MatDialogModule} from '@angular/material/dialog';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatInputModule,
    MatStepperModule,
    MatRadioModule,
    MatSelectModule,
    MatTableModule,
    MatDialogModule
  ],
  exports: [
    MatInputModule,
    MatStepperModule,
    MatRadioModule,
    MatSelectModule,
    MatTableModule,
    MatDialogModule
  ]
})
export class MaterialModule { }
