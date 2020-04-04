import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RequestPatchStatusEnum } from 'src/app/common/helper/enums';

@Injectable({
  providedIn: 'root'
})
export class MyRequestsService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  getMyRequests(){
    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'myrequests', {withCredentials: false});
  }

  cancelRequest(id: number){
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'myrequests/' + id, {withCredentials: false});
  }

  closeRequest(id: number){
    return this._httpClient.patch(this._utilitiesService.getAPIUrl() + 'myrequests/' + id,{
      "newRequestStatus": RequestPatchStatusEnum[RequestPatchStatusEnum.Closed]
    }, {withCredentials: false});
  }
}
