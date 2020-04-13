import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { SetNewPasswordDTO } from 'src/app/common/models/setNewPasswordDTO';
import { UserService } from '../user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  hidePW = false;
  showError = false;
  
  changePasswordCredentials: SetNewPasswordDTO;

  constructor(public dialogRef: MatDialogRef<ChangePasswordComponent>, private _userService: UserService) {
    this.changePasswordCredentials = new SetNewPasswordDTO;
  }

  ngOnInit(): void {
  }

  onCancel(): void {
    this.dialogRef.close("Cancel");
  }

  onSave(): void {
    this.showError = false;
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
