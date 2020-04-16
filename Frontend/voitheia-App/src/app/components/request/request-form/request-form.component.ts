import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { RequestDTO } from 'src/app/common/models/requestDTO';
import { RequestService } from '../request.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { RequestTypeEnum } from 'src/app/common/helper/enums/request-type.enum';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { TranslateService } from '@ngx-translate/core';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { UserService } from '../../user/user.service';
import { UserDTO } from 'src/app/common/models/userDTO';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {
  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;

  termsChecked = false;

  request: RequestDTO;

  constructor(private _requestService: RequestService,
              private _dialogService: DialogService,
              private _navigationService: NavigationService,
              private _utilitiesService: UtilitiesService,
              private _translateService: TranslateService,
              private _authService: AuthService,
              private _userService: UserService) {
    this.request = new RequestDTO();
  }

  ngOnInit(): void {
    if(!this._utilitiesService.isLive()){
      this._navigationService.navigateTo("countdown");
    }

    if(this._authService.isLoggedIn()){
      this._userService.getUserData().subscribe(
        data => {
          let userData = data as UserDTO;
          
          this.request.email = userData.email;
          this.request.firstName = userData.firstName;
          this.request.lastName = userData.lastName;
          this.request.street = userData.street;
          this.request.zip = userData.zip;
          this.request.city = userData.city;
        },
        err => {
          
        }
      );;
    }

  }

  public send(){
    this.request.type = this.RequestTypeEnum[this.request.type];
    
    this._requestService.createRequest(this.request).subscribe(
      data => {
        this._translateService.get(['Request.Create.Dialog.Text', 'Request.Create.Dialog.Buttons.NewRequest', 'General.Dialogs.Titles.Success', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogTwoButtons(res['General.Dialogs.Titles.Success'], res['Request.Create.Dialog.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close'], res['Request.Create.Dialog.Buttons.NewRequest'], function(){this._navigationService.navigateTo("home")}.bind(this), function(){this._navigationService.navigateTo("request/create")}.bind(this));
        });
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  number(input:string): number{
    return Number(input);
  }
}
