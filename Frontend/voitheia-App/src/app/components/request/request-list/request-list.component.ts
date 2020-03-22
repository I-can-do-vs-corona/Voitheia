import { Component, OnInit, ViewChild } from '@angular/core';
import { RequestService } from '../request.service';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatTableDataSource } from '@angular/material/table';
import { MatSelectChange } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { RequestViewComponent } from '../request-view/request-view.component';
import { RequestTypeEnum } from 'src/app/common/helper/enums';
import { MatPaginator } from '@angular/material/paginator';

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

  constructor(private _requestService: RequestService, public dialog: MatDialog) {
    
  }

  ngOnInit(): void {
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
    const dialogRef = this.dialog.open(RequestViewComponent, {
      width: '450px',
      data: {item: this.openedItem}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this._requestService.takeRequest(this.openedItem.id).subscribe(
          data => {
            alert("angenommen & gespeichert")
            this.loadAllData();
          },
          err => {
            alert("error");
          }
        );
      }else{
        this.openedItem = null;
      }
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
        alert("error");
      }
    );
  }
}
