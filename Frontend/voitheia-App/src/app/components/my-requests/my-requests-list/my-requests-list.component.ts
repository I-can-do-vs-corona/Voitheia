import { Component, OnInit } from '@angular/core';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatTableDataSource } from '@angular/material/table';
import { RequestTypeEnum, DialogIconTypeEnum } from 'src/app/common/helper/enums';
import { MyRequestsService } from '../my-requests.service';
import { MatDialog } from '@angular/material/dialog';
import { MyRequestsViewComponent } from '../my-requests-view/my-requests-view.component';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { environment } from 'src/environments/environment';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-my-requests-list',
  templateUrl: './my-requests-list.component.html',
  styleUrls: ['./my-requests-list.component.scss']
})
export class MyRequestsListComponent implements OnInit {

  openedItem: RequestResponseDTO;
  requestDataSource: MatTableDataSource<RequestResponseDTO>;
  displayedColumns = ['firstName', 'type', 'distanceToUser'];

  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;
  
  constructor(private _myRequestsService: MyRequestsService, private _dialog: MatDialog, private _utilitiesService: UtilitiesService, private _dialogService: DialogService, private _translateService: TranslateService) { }

  ngOnInit(): void {
      this.requestDataSource = new MatTableDataSource<RequestResponseDTO>();
      this.loadAllData();
  }

  openDetails(index:number){
    this.openedItem = this.requestDataSource.data[index];
    const dialogRef = this._dialog.open(MyRequestsViewComponent, {
      width: environment.dialogWidth,
      data: {item: this.openedItem}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == "Close"){
        this._myRequestsService.closeRequest(this.openedItem.id).subscribe(
          data => {
            this._translateService.get(['MyRequest.Details.Dialogs.Close.Title', 'MyRequest.Details.Dialogs.Close.Text']).subscribe((res: string) => {
              this._dialogService.showSimpleSuccessDialog(res['MyRequest.Details.Dialogs.Close.Title'], res['MyRequest.Details.Dialogs.Close.Text']);
            });
            this.loadAllData();
          },
          err => {
            this._utilitiesService.handleError(err);
          }
        );
      }else if(result == "Cancel"){
        this._myRequestsService.cancelRequest(this.openedItem.id).subscribe(
          data => {
            this._translateService.get(['MyRequest.Details.Dialogs.Cancel.Title', 'MyRequest.Details.Dialogs.Cancel.Text', 'General.Buttons.Close']).subscribe((res: string) => {
              this._dialogService.showDialogOneButton(res['MyRequest.Details.Dialogs.Cancel.Title'], res['MyRequest.Details.Dialogs.Cancel.Text'], DialogIconTypeEnum.Info, res['General.Buttons.Close']);
            });
            this.loadAllData();
          },
          err => {
            this._utilitiesService.handleError(err);
          }
        );
      }
      this.openedItem = null;
    });
  }

  private loadAllData(){
    this._myRequestsService.getMyRequests().subscribe(
      data => {
        this.requestDataSource.data = data['requests'];
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

}
