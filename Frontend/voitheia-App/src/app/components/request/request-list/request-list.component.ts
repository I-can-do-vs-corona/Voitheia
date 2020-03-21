import { Component, OnInit } from '@angular/core';
import { RequestService } from '../request.service';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatTableDataSource } from '@angular/material/table';
import { MatSelectChange } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { RequestViewComponent } from '../request-view/request-view.component';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {
  tmpdata: RequestResponseDTO[];
  requestDataSource: MatTableDataSource<RequestResponseDTO>;
  displayedColumns = ['firstName', 'type', 'distanceToUser'];

  constructor(private _requestService: RequestService, public dialog: MatDialog) {
    
  }

  ngOnInit(): void {
    this.requestDataSource = new MatTableDataSource<RequestResponseDTO>();
    this._requestService.getRequests().subscribe(
      data => {
        this.requestDataSource.data = data['requests'];
        this.tmpdata = data['requests'];
      },
      err => {
        alert("error");
      }
    );
  }

  applyFilter(event: MatSelectChange) {
    const filterValues = event.value;

    var tmp: RequestResponseDTO[];
    if(filterValues.length === 0){
      tmp = this.tmpdata;
    } else{
      tmp = this.tmpdata.filter(item => {
        for (let f of filterValues) {
          if(item.type === f){
            return true;
          }
        }
      });
    }

    this.requestDataSource.data = tmp;

    if (this.requestDataSource.paginator) {
      this.requestDataSource.paginator.firstPage();
    }
  }
  
  openDetails(index:number){
    var element = this.requestDataSource.data[index];
    const dialogRef = this.dialog.open(RequestViewComponent, {
      width: '250px',
      data: {element: element}
    });

    dialogRef.afterClosed().subscribe(result => {
      alert("closed");
    });
  }
}
