import { NgModule, FactorySansProvider } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faSave
} from '@fortawesome/free-solid-svg-icons';

library.add(
  faSave
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
export class CustomFontAwesomeModule {
  public faSave = faSave;
}
