import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RequestDTO } from 'src/app/common/models/requestDTO';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  createRequest(request: RequestDTO) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'request', request);
  }

  getRequests(requestAmount = 0, requestDistance = 0){
    var parameters = "?metersPerimeter=";
    parameters += (requestDistance === 0)? this._utilitiesService.getRequestDistance().toString() : requestDistance.toString();
    parameters += "&amount=";
    parameters += (requestAmount === 0)? this._utilitiesService.getRequestAmount().toString() : requestAmount.toString();
    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'request' + parameters);
  }

  takeRequest(id: number){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'myRequests', {"requestId" : id});
  }
}
