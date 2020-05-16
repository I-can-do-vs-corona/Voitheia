import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RequestStatusEnum } from 'src/app/common/helper/enums/request-status.enum';

@Injectable({
  providedIn: 'root'
})
export class MyRequestsService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  getMyAssignedRequests(assigned: boolean = true, closed: boolean = false){
    // Initialize Params Object
    let params = new HttpParams();
    
    // Begin assigning parameters
    params = params.append('assigned', assigned.toString());
    params = params.append('closed', closed.toString());

    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'myRequests/assigned', { params: params });
  }

  getMyCreatedRequests(open: boolean = true, assigned: boolean = true, closed: boolean = false){
    // Initialize Params Object
    let params = new HttpParams();
    
    // Begin assigning parameters
    params = params.append('open', open.toString());
    params = params.append('assigned', assigned.toString());
    params = params.append('closed', closed.toString());

    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'myRequests/created', { params: params });
  }

  cancelRequest(id: number){
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'myrequests/' + id);
  }

  closeRequest(id: number){
    return this._httpClient.patch(this._utilitiesService.getAPIUrl() + 'myrequests/' + id,{
      "newRequestStatus": RequestStatusEnum[RequestStatusEnum.Closed]
    });
  }
}
