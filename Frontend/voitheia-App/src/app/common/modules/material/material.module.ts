import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatInputModule} from '@angular/material/input';
import {MatStepperModule} from '@angular/material/stepper';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {MatTableModule, MatTableDataSource } from '@angular/material/table';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatInputModule,
    MatStepperModule,
    MatSelectModule,
    MatRadioModule,
    MatTableModule,
    MatTableDataSource 
  ],
  exports: [
    MatInputModule,
    MatStepperModule,
    MatSelectModule,
    MatRadioModule,
    MatTableModule,
    MatTableDataSource 
  ]
})
export class MaterialModule { }
