import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { AuthGuard } from '../../guards/auth.guard';
import { NavigationService } from '../services/navigation.service';
import { DialogService } from '../services/dialog.service';
import { UtilitiesService } from '../services/utilities.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        UtilitiesService,
        DialogService,
        NavigationService,
        AuthService,
        AuthGuard
      ]
    };
  }
}
