import { Component, OnInit } from '@angular/core';
import { RequestDTO } from 'src/app/common/models/requestDTO';
import { RequestService } from '../request.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { RequestTypeEnum, DialogIconTypeEnum } from 'src/app/common/helper/enums';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {

  faPaperPlane = faPaperPlane;
  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;

  request: RequestDTO;

  constructor(private _requestService: RequestService,
              private _dialogService: DialogService,
              private _navigationService: NavigationService,
              private _translate: TranslateService) {
    this.request = new RequestDTO();
  }

  ngOnInit(): void {
  }

  public send(){
    this.request.type = this.RequestTypeEnum[this.request.type];
    
    this._requestService.createRequest(this.request).subscribe(
      data => {
        this._translate.get(['Request.Create.Dialog.Text', 'Request.Create.Dialog.Buttons.NewRequest', 'General.Dialogs.Titles.Success', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogTwoButtons(res['General.Dialogs.Titles.Success'], res['Request.Create.Dialog.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close'], res['Request.Create.Dialog.Buttons.NewRequest'], function(){this._navigationService.navigateTo("home")}.bind(this), function(){this._navigationService.navigateTo("request/create")}.bind(this));
        });
      },
      err => {
        this._translate.get(['General.Dialogs.Title.Error', 'General.Dialogs.Text.Error', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['General.Dialogs.Title.Error'], res['General.Dialogs.Text.Error'] + "<br />" + err, DialogIconTypeEnum.Error, res['General.Buttons.Close']);
        });
        
      }
    );
  }
}
