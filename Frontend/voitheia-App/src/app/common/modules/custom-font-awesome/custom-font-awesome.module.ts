import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faLock
} from '@fortawesome/free-solid-svg-icons';

library.add(
  faLock
)

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FontAwesomeModule 
  ],
  exports: [
    FontAwesomeModule
  ]
})
export class CustomFontAwesomeModule { }
