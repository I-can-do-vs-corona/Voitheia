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

  openRequestsAssignedToMeDataSource: MatTableDataSource<MyRequestDTO>;
  openRequestsCreatedByMeDataSource: MatTableDataSource<MyRequestDTO>;
  closedRequestsDataSource: MatTableDataSource<MyRequestDTO>;

  openRequestsAssignedMeDisplayedColumns = ['firstName', 'type', 'distanceToUser', 'user'];
  openRequestsCreatedByMeDisplayedColumns = ['firstName', 'type', 'status', 'user'];
  closedRequestsDisplayedColumns = ['firstName', 'type', 'author', 'status', 'user'];

  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;
  
  constructor(private _myRequestsService: MyRequestsService, private _dialog: MatDialog, private _utilitiesService: UtilitiesService, private _dialogService: DialogService, private _translateService: TranslateService, private _navigationService: NavigationService) { }

  ngOnInit(): void {
    if(!this._utilitiesService.isLive()){
      this._navigationService.navigateTo("countdown");
    }
    
    this.openRequestsAssignedToMeDataSource = new MatTableDataSource<MyRequestDTO>();
    this.openRequestsCreatedByMeDataSource = new MatTableDataSource<MyRequestDTO>();
    this.closedRequestsDataSource = new MatTableDataSource<MyRequestDTO>();
    this.loadOpenRequestsAssignedToMe();
  }

  openDetails(id:number){
    this.openedItem = null;

    this.openedItem = this.openRequestsCreatedByMeDataSource.data.find(req => req.id === id);
    if(typeof this.openedItem === 'undefined' || this.openedItem === null){
      this.openedItem = this.openRequestsAssignedToMeDataSource.data.find(req => req.id === id);
    }
    if(typeof this.openedItem === 'undefined' || this.openedItem === null){
      this.openedItem = this.closedRequestsDataSource.data.find(req => req.id === id);
    }

    if(typeof this.openedItem !== 'undefined'){
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
              this.loadOpenRequestsAssignedToMe();
              this.loadClosedRequests();
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
              this.loadOpenRequestsAssignedToMe();
            },
            err => {
              this._utilitiesService.handleError(err);
            }
          );
        }
        this.openedItem = null;
      });
    }
  }

  tabClick(tab) {
    if(tab.index === 0 && this.openRequestsAssignedToMeDataSource.data !== null && this.openRequestsAssignedToMeDataSource.data.length === 0){
      this.loadOpenRequestsAssignedToMe();
    }
    else if(tab.index === 1 && this.openRequestsCreatedByMeDataSource.data !== null && this.openRequestsCreatedByMeDataSource.data.length === 0){
      this.loadOpenRequestsCreatedByMe();
    }
    else if(tab.index === 2 && this.closedRequestsDataSource.data !== null && this.closedRequestsDataSource.data.length === 0){
      this.loadClosedRequests();
    }
  }

  private loadOpenRequestsAssignedToMe(){
    this._myRequestsService.getMyAssignedRequests().subscribe(
      data => {
        this.openRequestsAssignedToMeDataSource.data = data['requests'];
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

  private loadOpenRequestsCreatedByMe(){
    this._myRequestsService.getMyCreatedRequests().subscribe(
      data => {
        this.openRequestsCreatedByMeDataSource.data = data['requests'];
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
  
  private loadClosedRequests(){
    this.closedRequestsDataSource = new MatTableDataSource<MyRequestDTO>();
    this._myRequestsService.getMyAssignedRequests(false, true).subscribe(
      data => {
        this.closedRequestsDataSource.data.push.apply(this.closedRequestsDataSource.data, data['requests']);
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );

    this._myRequestsService.getMyCreatedRequests(false, false, true).subscribe(
      data => {
        this.closedRequestsDataSource.data.push.apply(this.closedRequestsDataSource.data, data['requests']);
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
}
