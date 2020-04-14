import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from './dialog/dialog.service';
import { DialogIconTypeEnum } from '../../helper/enums/dialog-icon-type.enum';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {

  public passwordRegEx: string;

  constructor(private _translate: TranslateService, private _dialogService: DialogService) {
    this.passwordRegEx = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-ZäöåüÅÖÄÜ]).{8,}$';
  }

  getAPIUrl() {
    return environment.apiBaseUrl;
  }

  getRequestAmount() {
    return environment.requestAmount
  }

  getRequestDistance() {
    return environment.requestDistance
  }

  handleError(error: any){
    this._translate.get(['General.Dialogs.Titles.Error', 'General.Dialogs.Text.Error', 'General.Buttons.Close']).subscribe((res: string) => {
      var errorText = (typeof error === 'string')? error : error.message;
      this._dialogService.showDialogOneButton(res['General.Dialogs.Titles.Error'], res['General.Dialogs.Text.Error'] + "<br />" + errorText, DialogIconTypeEnum.Error, res['General.Buttons.Close']);
    });
  }
}
