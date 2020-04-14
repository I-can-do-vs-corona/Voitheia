import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { SetNewPasswordDTO } from 'src/app/common/models/setNewPasswordDTO';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  hidePW = true;
  showError = false;

  passwordRegEx = '';
  
  changePasswordCredentials: SetNewPasswordDTO;

  constructor(public dialogRef: MatDialogRef<ChangePasswordComponent>, private _userService: UserService, private _utilitiesService: UtilitiesService) {
    this.changePasswordCredentials = new SetNewPasswordDTO;
    this.passwordRegEx = _utilitiesService.passwordRegEx;
  }

  ngOnInit(): void {
  }

  onCancel(): void {
    this.dialogRef.close("Cancel");
  }

  onSave(): void {
    this.showError = false;
    debugger;
    this._userService.changePassword(this.changePasswordCredentials).subscribe(
      data => {
        this.dialogRef.close("Success");
      },
      err => {
        this.showError = true;
      }
    )
  }

  dismissAlert(){
    this.showError = false;
  }

}
