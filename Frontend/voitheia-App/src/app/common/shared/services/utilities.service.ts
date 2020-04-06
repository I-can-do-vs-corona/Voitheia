import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from './dialog/dialog.service';
import { DialogIconTypeEnum } from '../../helper/enums';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {

  constructor(private _translate: TranslateService, private _dialogService: DialogService) { }

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
    this._translate.get(['General.Dialogs.Title.Error', 'General.Dialogs.Text.Error', 'General.Buttons.Close']).subscribe((res: string) => {
      this._dialogService.showDialogOneButton(res['General.Dialogs.Title.Error'], res['General.Dialogs.Text.Error'] + "<br />" + error, DialogIconTypeEnum.Error, res['General.Buttons.Close']);
    });
  }
}
