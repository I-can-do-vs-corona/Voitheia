import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';
import { FilesizePipe } from 'src/app/common/helper/pipes/filesize.pipe';
import { UserService } from '../../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-profile-picture-upload',
  templateUrl: './profile-picture-upload.component.html',
  styleUrls: ['./profile-picture-upload.component.scss']
})
export class ProfilePictureUploadComponent implements OnInit {
  readyToUpload: boolean;

  selectedFile: File;
  fileExtension: string;
  fileAsBase64String: any;
  fileBase64RawData: string;
  fileWidth: number;
  fileHeight: number;

  imageError: string;

  maxFileSize: number;
  maxFileWidth: number;
  maxFileHeight: number;

  constructor(public dialogRef: MatDialogRef<ProfilePictureUploadComponent>, private _userService: UserService, private _utilitiesService: UtilitiesService) { }

  ngOnInit(): void {
    this.readyToUpload = false;
    this.selectedFile = null;
    this.fileExtension = "";
    this.imageError = "";

    this.maxFileWidth = environment.profilePictureMaxWidth;
    this.maxFileHeight = environment.profilePictureMaxHeight;
    this.maxFileSize = environment.profilePictureMaxSize;
  }

  onFileChanged(event) {
    this.imageError = "";
    this.readyToUpload = false;
    if (event.target.files && event.target.files[0]) {

      if (event.target.files[0].size > environment.profilePictureMaxSize) {
        this.imageError = "fileSize";
        return false;
      }

      if (!environment.profilePictureAllowedTypes.some( vendor => vendor === event.target.files[0].type )) {
          this.imageError = "fileExtension";
          return false;
      }

      const readerTest = new FileReader();
      readerTest.onload = (e: any) => {
        const image = new Image();
        image.src = e.target.result;
        image.onload = rs => {
          this.fileHeight = rs.currentTarget['height'];
          this.fileWidth = rs.currentTarget['width'];

          if (this.fileHeight > environment.profilePictureMaxHeight && this.fileWidth > environment.profilePictureMaxWidth) {
            this.imageError = "fileDimensions";
            return false;
          } else {
            this.selectedFile = event.target.files[0];
            this.fileExtension = this.selectedFile.type.split('/').pop();
            this.fileBase64RawData = e.target.result.split("data:" + this.selectedFile.type + ";base64,").pop();
            this.readyToUpload = true;
          }
        };
      };

      readerTest.readAsDataURL(event.target.files[0]);
    }
  }

  upload(){
    this._userService.updateProfilePicture(this.selectedFile).subscribe(
      data => {
        this.dialogRef.close("Success");
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    )
  }

  deleteSelectedFile(){
    this.selectedFile = null;
    this.fileExtension = "";
    this.fileHeight = 0;
    this.fileWidth = 0;
    this.readyToUpload = false;
  }
}
