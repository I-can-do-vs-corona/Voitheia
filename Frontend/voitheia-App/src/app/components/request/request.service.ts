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
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'request', request, {withCredentials: false});
  }
}
