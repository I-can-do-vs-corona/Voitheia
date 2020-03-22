import { Component, OnInit } from '@angular/core';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatTableDataSource } from '@angular/material/table';
import { RequestTypeEnum } from 'src/app/common/helper/enums';
import { MyRequestsService } from '../my-requests.service';
import { MatDialog } from '@angular/material/dialog';
import { MyRequestsViewComponent } from '../my-requests-view/my-requests-view.component';

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
  
  constructor(public _myRequestsService: MyRequestsService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.requestDataSource = new MatTableDataSource<RequestResponseDTO>();
    this.loadAllData();
  }

  openDetails(index:number){
    this.openedItem = this.requestDataSource.data[index];
    const dialogRef = this.dialog.open(MyRequestsViewComponent, {
      width: '450px',
      data: {item: this.openedItem}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == "Close"){
        this._myRequestsService.closeRequest(this.openedItem.id).subscribe(
          data => {
            alert("Auftrag geschlossen")
            this.loadAllData();
          },
          err => {
            alert("error");
          }
        );
      }else if(result == "Cancel"){
        this._myRequestsService.cancelRequest(this.openedItem.id).subscribe(
          data => {
            alert("Auftrag abgebrochen")
            this.loadAllData();
          },
          err => {
            alert("error");
          }
        );
      }else {
        this.openedItem = null;
      }
    });
  }

  private loadAllData(){
    this._myRequestsService.getMyRequests().subscribe(
      data => {
        this.requestDataSource.data = data['requests'];
      },
      err => {
        alert("error");
      }
    );
  }

}
