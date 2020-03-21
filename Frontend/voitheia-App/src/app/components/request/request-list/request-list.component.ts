import { Component, OnInit } from '@angular/core';
import { RequestService } from '../request.service';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {

  public listRequests : RequestResponseDTO[];
  displayedColumns = ['firstName', 'lastName', 'description', 'distanceToUser'];

  constructor(private _requestService: RequestService) {
    this._requestService.getRequests().subscribe(
      data => {
        this.listRequests = data['requests'];
      },
      err => {
        alert("error");
      }
    );
  }

  ngOnInit(): void {
  }

}
