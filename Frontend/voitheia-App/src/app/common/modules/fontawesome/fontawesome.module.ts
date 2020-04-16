import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faPaperPlane, faUser, faPowerOff, faEdit, faTrashAlt, faKey, faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import { faEyeSlash, faEye, faEnvelope } from '@fortawesome/free-regular-svg-icons';



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
export class CustomFontawesomeModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(faPaperPlane, faUser, faPowerOff, faEdit, faTrashAlt, faKey, faInfoCircle);
    library.addIcons(faEye, faEyeSlash, faEnvelope);
  }
}
