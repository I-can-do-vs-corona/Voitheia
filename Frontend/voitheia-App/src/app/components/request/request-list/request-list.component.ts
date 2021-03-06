import { Component, OnInit, ViewChild } from '@angular/core';
import { RequestService } from '../request.service';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatTableDataSource } from '@angular/material/table';
import { MatSelectChange } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { RequestViewComponent } from '../request-view/request-view.component';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums/dialog-icon-type.enum';
import { RequestTypeEnum } from 'src/app/common/helper/enums/request-type.enum';
import { MatPaginator } from '@angular/material/paginator';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { environment } from 'src/environments/environment';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {
  unfilteredResult: RequestResponseDTO[];
  openedItem: RequestResponseDTO;
  requestDataSource: MatTableDataSource<RequestResponseDTO>;
  displayedColumns = ['firstName', 'type', 'distanceToUser'];
  resultsLength = 0;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;

  constructor(private _requestService: RequestService,
              private _utilitiesService: UtilitiesService,
              private _dialogService: DialogService,
              private _translateService: TranslateService,
              private _navigationService: NavigationService,
              private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    if(!this._utilitiesService.isLive()){
      this._navigationService.navigateTo("countdown");
    }
    this.requestDataSource = new MatTableDataSource<RequestResponseDTO>();
    this.requestDataSource.paginator = this.paginator;
    this.loadAllData();
  }

  applyFilter(event: MatSelectChange) {
    const filterValues = event.value;

    var tmp: RequestResponseDTO[];
    if(filterValues.length === 0){
      tmp = this.unfilteredResult;
    } else{
      tmp = this.unfilteredResult.filter(item => {
        for (let f of filterValues) {
          if(item.type === RequestTypeEnum[f]){
            return true;
          }
        }
      });
    }

    this.requestDataSource.data = tmp;
    this.resultsLength = tmp.length;

    if (this.requestDataSource.paginator) {
      this.requestDataSource.paginator.firstPage();
    }
  }
  
  openDetails(index:number){
    this.openedItem = this.requestDataSource.data[index];
    const dialogRef = this._dialog.open(RequestViewComponent, {
      width: environment.dialogWidth,
      data: {item: this.openedItem}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this._requestService.takeRequest(this.openedItem.id).subscribe(
          data => {
            this._translateService.get(['Request.Details.Dialogs.Title', 'Request.Details.Dialogs.Text', 'Request.Details.Dialogs.MyRequestsButton', 'General.Buttons.Close']).subscribe((res: string) => {
              this._dialogService.showDialogTwoButtons(res['Request.Details.Dialogs.Title'], res['Request.Details.Dialogs.Text'], DialogIconTypeEnum.Success, res['Request.Details.Dialogs.MyRequestsButton'], res['General.Buttons.Close'], function(){this._navigationService.navigateTo("my-requests/list")}.bind(this));
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
    this._requestService.getRequests().subscribe(
      data => {
        this.requestDataSource.data = data['requests'];
        this.unfilteredResult = data['requests'];
        this.resultsLength = this.unfilteredResult.length;
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
}
