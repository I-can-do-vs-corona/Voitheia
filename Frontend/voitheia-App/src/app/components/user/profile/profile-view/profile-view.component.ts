import { Component, OnInit } from '@angular/core';
import { UserService } from '../../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { UserDTO } from 'src/app/common/models/userDTO';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ProfileEditComponent } from '../profile-edit/profile-edit.component';
import { environment } from 'src/environments/environment';
import { ChangePasswordComponent } from '../../change-password/change-password.component';

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.scss']
})
export class ProfileViewComponent implements OnInit {

  userData: UserDTO;
  sessionValidUntil: Date;

  constructor(private _userService: UserService,
              private _utilitiesService:UtilitiesService,
              private _translateService: TranslateService,
              private _dialogService: DialogService,
              private _authService: AuthService,
              private _dialog: MatDialog) { }

  ngOnInit(): void {
    this.userData = new UserDTO();
    
    this.sessionValidUntil = new Date(localStorage.getItem("validUntil"));

    this.loadUserData();
  }

  openDeleteAccountDialog(){
    this._translateService.get(['User.Profile.View.Dialogs.Delete.Title', 'User.Profile.View.Dialogs.Delete.Text', 'General.Buttons.Delete', 'General.Buttons.Cancel']).subscribe((res: string) => {
      this._dialogService.showDialogTwoButtons(res['User.Profile.View.Dialogs.Delete.Title'], res['User.Profile.View.Dialogs.Delete.Text'], DialogIconTypeEnum.Question, res['General.Buttons.Delete'], res['General.Buttons.Cancel'], function(){this.deleAccount()}.bind(this));
    });
  }

  openEditUserDataDialog(){
    const dialogRef = this._dialog.open(ProfileEditComponent, {
      width: environment.dialogWidth,
      data: {userData: this.userData}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadUserData();
    });
  }

  resendEmailConfirmationMail(){
    this._userService.sendConfirmationMailAgain(this.userData.email).subscribe(
      data => {
        this._translateService.get(['User.Profile.View.Dialogs.resendConfirmationMail.Title', 'User.Profile.View.Dialogs.resendConfirmationMail.Text', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['User.Profile.View.Dialogs.resendConfirmationMail.Title'], res['User.Profile.View.Dialogs.resendConfirmationMail.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close']);
        });
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  openSetPasswordDialog(){
    const dialogRef = this._dialog.open(ChangePasswordComponent, {
      width: environment.dialogWidth
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === "Success"){
        this._translateService.get(['User.Profile.View.Dialogs.Deleted.Title', 'User.Profile.View.Dialogs.Deleted.Text', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['User.Profile.View.Dialogs.Deleted.Title'], res['User.Profile.View.Dialogs.Deleted.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close'], function(){this._authService.logout()}.bind(this));
        });
      }
    });
  }

  private deleAccount(){
    this._userService.deleteAccount().subscribe(
      data => {
        this._translateService.get(['User.Profile.View.Dialogs.Deleted.Title', 'User.Profile.View.Dialogs.Deleted.Text', 'General.Buttons.Close']).subscribe((res: string) => {
          this._dialogService.showDialogOneButton(res['User.Profile.View.Dialogs.Deleted.Title'], res['User.Profile.View.Dialogs.Deleted.Text'], DialogIconTypeEnum.Success, res['General.Buttons.Close'], function(){this._authService.logout()}.bind(this));
        });
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  private loadUserData(){
    this._userService.getUserData().subscribe(
      data => {
        this.userData = data as UserDTO;
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
}
