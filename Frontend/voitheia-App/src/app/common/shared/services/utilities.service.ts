import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from './dialog/dialog.service';
import { DialogIconTypeEnum } from '../../helper/enums/dialog-icon-type.enum';
import * as moment from 'moment';
import { UserDTO } from '../../models/userDTO';
import { UserService } from 'src/app/components/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {

  public passwordRegEx: string;

  userData: UserDTO;

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
      let errorText = "";
      if(typeof error === 'string'){
        errorText = error;
      }
      else if(typeof error.error !== 'undefined' && Array.isArray(error.error)){
        errorText = error.error[0].errormessage;
      }
      else if(typeof error.error !== 'undefined' && typeof error.error.errormessage !== 'undefined' && error.error.errormessage !== ""){
        errorText = error.error.errormessage;
      }
      else {
        errorText = error.message;
      }

      this._dialogService.showDialogOneButton(res['General.Dialogs.Titles.Error'], res['General.Dialogs.Text.Error'] + "<br />" + errorText, DialogIconTypeEnum.Error, res['General.Buttons.Close']);
    });
  }

  isLive(): boolean{
    return moment(environment.goLiveDate).isSameOrBefore();
  }

  isRegistrationOpen(): boolean{
    return moment(environment.registerOpenDate).isSameOrBefore();
  }
}
