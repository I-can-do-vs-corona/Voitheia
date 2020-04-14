import { Injectable } from '@angular/core';
import { DialogComponent } from './dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog: MatDialog, private _translate: TranslateService) { }

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

  showSimpleSuccessDialog(title: string, content: string){
    this._translate.get(['General.Buttons.Close']).subscribe((res: string) => {
      this.showDialogOneButton(title, content, DialogIconTypeEnum.Success, res['General.Buttons.Close']);
    });
  }
}
