import { Component, OnInit } from '@angular/core';
import { ResetPasswordCredentials } from 'src/app/common/models/resetPasswordCredentialsDTO';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from '../user.service';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordMailSent = false;
  passwordReseted = false;
  isSendResetMail = true;
  hidePW = true;
  emailFromUrl = false;
  email = '';
  passwordRegEx = '';
  resetPasswordCredentials = new ResetPasswordCredentials();
  
  reCaptchaKey = '';
  reCaptchaValid = false;

  constructor(private _userService: UserService,
    private _utilitiesService: UtilitiesService,
    private _dialogService: DialogService,
    private _navigationService: NavigationService,
    private _translateService: TranslateService) { }

  ngOnInit(): void {
    this.passwordRegEx = this._utilitiesService.passwordRegEx;

    this.reCaptchaKey = environment.reCaptchaKey;
    if (!this.reCaptchaKey || this.reCaptchaKey === '') {
      this.reCaptchaValid = true;
    }

    if (this._navigationService.routeParameterIsSet("token")) {
      this.resetPasswordCredentials.token = this._navigationService.getRouteParameter('token');
      this.isSendResetMail = false;

      if (this._navigationService.routeParameterIsSet("email")) {
        this.resetPasswordCredentials.email = this._navigationService.getRouteParameter('email');
        this.emailFromUrl = true;
      }
    }
  }

  sendMail() {
    this._userService.sendResetPasswordMail(this.email).subscribe(
      data => {
        this.resetPasswordMailSent = true;
        this._translateService.get(['User.ResetPassword.Dialogs.ResetMailSent.Title', 'User.View.Dialogs.ResetMailSent.Text', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['User.ResetPassword.Dialogs.ResetMailSent.Title'], res['User.ResetPassword.Dialogs.ResetMailSent.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close']);
        });
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  resetPassword() {
    this._userService.resetPassword(this.resetPasswordCredentials).subscribe(
      data => {
        this.passwordReseted = true;
        this._translateService.get(['User.ResetPassword.Dialogs.PasswordReseted.Title', 'User.ResetPassword.Dialogs.PasswordReseted.Text', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['User.ResetPassword.Dialogs.PasswordReseted.Title'], res['User.ResetPassword.Dialogs.PasswordReseted.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close'], function(){this._navigationService.navigateTo('login')}.bind(this));
        });
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  handleCorrectCaptcha(event) {
    this.reCaptchaValid = true;
  }

  handleExpiredCaptcha() {
    this.reCaptchaValid = false;
  }

  getreCaptchaLanguage() {
    if (this._translateService.currentLang === 'de') {
      return 'de';
    } else if (this._translateService.currentLang === 'se') {
      return 'sv';
    }
    return 'en';
  }
}
