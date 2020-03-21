import { Component, OnInit } from '@angular/core';
import { RequestDTO } from 'src/app/common/models/requestDTO';
import { RequestService } from '../request.service';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {

  request: RequestDTO;

  constructor(private _requestService: RequestService) {
    this.request = new RequestDTO();
  }

  ngOnInit(): void {
  }

  public send(){

  }
}
