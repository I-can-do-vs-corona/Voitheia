import { Component, OnInit } from '@angular/core';
import { RequestDTO } from 'src/app/common/models/requestDTO';
import { RequestService } from '../request.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { RequestTypeEnum } from 'src/app/common/helper/enums';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {

  faPaperPlane = faPaperPlane;
  RequestTypeEnum: typeof RequestTypeEnum = RequestTypeEnum;

  request: RequestDTO;

  constructor(private _requestService: RequestService, private _navigationService: NavigationService) {
    this.request = new RequestDTO();
  }

  ngOnInit(): void {
  }

  public send(){
    this.request.type = this.RequestTypeEnum[this.request.type];
    
    this._requestService.createRequest(this.request).subscribe(
      data => {
        this._navigationService.navigateTo("request/create/success")
      },
      err => {
        alert("error");
      }
    );
  }
}
