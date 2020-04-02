import { Injectable } from '@angular/core';
import { DialogComponent } from './dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog: MatDialog) { }

  test(){
    const dialogRef = this._dialog.open(DialogComponent, {
      width: environment.dialogWidth
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === "Primary"){
        alert("Primary");
      }else if(result === "Secondary"){
        alert("Secondary");
      }
    });
  }

  showDialogOneButton(title: string, content: string, dialogIconType: DialogIconTypeEnum, primaryButtonText: string, primaryButtonCallback: Function = null){
    const dialogRef = this._dialog.open(DialogComponent, {
      width: environment.dialogWidth
    });

    dialogRef.componentInstance.iconType = dialogIconType;
    dialogRef.componentInstance.title = title;
    dialogRef.componentInstance.content = content;
    dialogRef.componentInstance.primaryButtonText = primaryButtonText;

    dialogRef.afterClosed().subscribe(result => {
      if(result === "Primary"){
        if(primaryButtonCallback !== null){
          primaryButtonCallback();
        }
      }
    });
  }

  showDialogTwoButtons(title: string, content: string, dialogIconType: DialogIconTypeEnum, primaryButtonText: string, secondaryButtonText: string, primaryButtonCallback: Function = null, secondaryButtonCallback: Function = null){
    const dialogRef = this._dialog.open(DialogComponent, {
      width: environment.dialogWidth
    });

    dialogRef.componentInstance.iconType = dialogIconType;
    dialogRef.componentInstance.title = title;
    dialogRef.componentInstance.content = content;
    dialogRef.componentInstance.primaryButtonText = primaryButtonText;
    dialogRef.componentInstance.secondaryButtonText = secondaryButtonText;

    dialogRef.afterClosed().subscribe(result => {
      if(result === "Primary"){
        if(primaryButtonCallback !== null){
          primaryButtonCallback();
        }
      }else if(result === "Secondary"){
        if(secondaryButtonCallback !== null){
          secondaryButtonCallback();
        }
      }
    });
  }
}
