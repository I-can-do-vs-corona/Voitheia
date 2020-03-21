import { Component, OnInit } from '@angular/core';
import { RequestDTO } from 'src/app/common/models/requestDTO';
import { RequestService } from '../request.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {

  faPaperPlane = faPaperPlane;

  request: RequestDTO;

  constructor(private _requestService: RequestService) {
    this.request = new RequestDTO();
  }

  ngOnInit(): void {
  }

  public send(){
    this._requestService.createRequest(this.request).subscribe(
      data => {
        alert("Anfrage entegegen genommen. Danke")
      },
      err => {
        alert("error");
      }
    );
  }
}
