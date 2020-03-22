import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { User } from 'src/app/common/models/User';


@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  registerUser(user: User) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/register', user, {withCredentials: false});
  }
}