import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { faExclamation, faQuestion, faInfo } from '@fortawesome/free-solid-svg-icons';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {
  
  //Variables
  title: string;
  content: string;
  primaryButtonText: string;
  secondaryButtonText: string;
  closeButtonText: string;
  iconType: DialogIconTypeEnum;

  //FontAwesome icons
  faExclamation = faExclamation;
  faQuestion = faQuestion;
  faInfo = faInfo;

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>) {
      this.iconType = DialogIconTypeEnum.None;
    }

  close(repsonse: string){
    this.dialogRef.close(repsonse);
  }

  ngOnInit(): void {
  }

  isUndefined(variable: any){
    return ((typeof variable === 'undefined') || variable === "");
  }

}
