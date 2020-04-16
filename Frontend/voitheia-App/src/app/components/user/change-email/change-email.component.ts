import { Component, OnInit } from '@angular/core';
import { SetNewEmailDTO } from 'src/app/common/models/setNewEmailDTO';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.scss']
})
export class ChangeEmailComponent implements OnInit {
  showError = false;
  
  changeEmailCredentials: SetNewEmailDTO;

  constructor(public dialogRef: MatDialogRef<ChangeEmailComponent>, private _userService: UserService) {
    this.changeEmailCredentials = new SetNewEmailDTO;
  }

  ngOnInit(): void {
  }

  onCancel(): void {
    this.dialogRef.close("Cancel");
  }

  onSave(): void {
    this.showError = false;
    this._userService.changeEmail(this.changeEmailCredentials).subscribe(
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
