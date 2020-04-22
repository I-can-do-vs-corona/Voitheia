import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RequestStatusEnum } from 'src/app/common/helper/enums/request-status.enum';

@Injectable({
  providedIn: 'root'
})
export class MyRequestsService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  getMyRequests(){
    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'myrequests/getAllComplex', {withCredentials: false});
  }

  cancelRequest(id: number){
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'myrequests/' + id, {withCredentials: false});
  }

  closeRequest(id: number){
    return this._httpClient.patch(this._utilitiesService.getAPIUrl() + 'myrequests/' + id,{
      "newRequestStatus": RequestStatusEnum[RequestStatusEnum.Closed]
    }, {withCredentials: false});
  }
}
