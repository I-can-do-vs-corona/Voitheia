import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { MyRequestsService } from '../my-requests.service';
import { MatDialog } from '@angular/material/dialog';
import { MyRequestsViewComponent } from '../my-requests-view/my-requests-view.component';
import { environment } from 'src/environments/environment';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { TranslateService } from '@ngx-translate/core';
import { RequestTypeEnum } from 'src/app/common/helper/enums/request-type.enum';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { MyRequestDTO } from 'src/app/common/models/myRequestDTO';
import { RequestStatusEnum } from 'src/app/common/helper/enums/request-status.enum';

@Component({
  selector: 'app-my-requests-list',
  templateUrl: './my-requests-list.component.html',
  styleUrls: ['./my-requests-list.component.scss']
})
export class MyRequestsListComponent implements OnInit {

  openedItem: MyRequestDTO;

  allRequests: MyRequestDTO[];

  openRequestsCreatedByMeDataSource: MatTableDataSource<MyRequestDTO>;
  openRequestsAcceptedByMeDataSource: MatTableDataSource<MyRequestDTO>;
  closedRequestsDataSource: MatTableDataSource<MyRequestDTO>;

  openRequestsCreatedByMeDisplayedColumns = ['firstName', 'type', 'user'];
  openRequestsAcceptedByMeDisplayedColumns = ['firstName', 'type', 'distanceToUser', 'user'];
  closedRequestsDisplayedColumns = ['firstName', 'type', 'author', 'status', 'user'];

  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;
  
  constructor(private _myRequestsService: MyRequestsService, private _dialog: MatDialog, private _utilitiesService: UtilitiesService, private _dialogService: DialogService, private _translateService: TranslateService, private _navigationService: NavigationService) { }

  ngOnInit(): void {
    if(!this._utilitiesService.isLive()){
      this._navigationService.navigateTo("countdown");
    }
    
    this.openRequestsCreatedByMeDataSource = new MatTableDataSource<MyRequestDTO>();
    this.openRequestsAcceptedByMeDataSource = new MatTableDataSource<MyRequestDTO>();
    this.closedRequestsDataSource = new MatTableDataSource<MyRequestDTO>();
    this.loadAllData();
  }

  openDetails(id:number){
    this.openedItem = this.allRequests.find(req => req.id === id);
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
        this.allRequests = data['requests'];

        this.openRequestsCreatedByMeDataSource.data = this.allRequests.filter(req => req.author === true && (req.status === RequestStatusEnum[RequestStatusEnum.Open] || req.status === RequestStatusEnum[RequestStatusEnum.Assigned]));
        this.openRequestsAcceptedByMeDataSource.data = this.allRequests.filter(req => req.author === false && req.status === RequestStatusEnum[RequestStatusEnum.Assigned]);
        this.closedRequestsDataSource.data = this.allRequests.filter(req => req.status === RequestStatusEnum[RequestStatusEnum.Closed]);
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
}
