import { Component, OnInit } from '@angular/core';
import { RequestService } from '../request.service';
import { RequestResponseDTO } from 'src/app/common/models/RequestResponseDTO';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {

  public data : RequestResponseDTO[];

  constructor(private _requestService: RequestService) {
    
  }

  ngOnInit(): void {
  }

}
