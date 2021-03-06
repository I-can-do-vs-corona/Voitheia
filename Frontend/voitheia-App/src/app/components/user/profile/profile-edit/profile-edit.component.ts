import { Component, OnInit, Inject, ViewChild, AfterViewInit } from '@angular/core';
import { UpdateUserDTO } from 'src/app/common/models/updateUserDTO';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserDTO } from 'src/app/common/models/userDTO';
import { UserService } from '../../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit, AfterViewInit{

  userData: UpdateUserDTO;
  showSuccess = false;
  
  @ViewChild('EditProfileForm') editProfileForm: HTMLFormElement;

  constructor(public dialogRef: MatDialogRef<ProfileEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDTO,
    private _userService: UserService,
    private _utilitiesService: UtilitiesService) {
      this.userData = new UpdateUserDTO();
      this.userData.firstName = (data["userData"] as UserDTO).firstName;
      this.userData.lastName = (data["userData"] as UserDTO).lastName;
      this.userData.street = (data["userData"] as UserDTO).street;
      this.userData.zip = (data["userData"] as UserDTO).zip;
      this.userData.city = (data["userData"] as UserDTO).city;
      //this.userData.country = (data["userData"] as UserDTO).country;
  }

  onCancel(): void {
    this.dialogRef.close("Cancel");
  }

  onSave(): void {
    this.showSuccess = false;
    this._userService.updateUserData(this.userData).subscribe(
      data => {
        this.showSuccess = true;
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    )
  }

  dismissAlert(){
    this.showSuccess = false;
  }

  ngOnInit(): void {
    
  }

  ngAfterViewInit(){
    setTimeout(()=>{
      this.editProfileForm.form.controls['zipcodeInput'].touched = true;
    });
  }

}
