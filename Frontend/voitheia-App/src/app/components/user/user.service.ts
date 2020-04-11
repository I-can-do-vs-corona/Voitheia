import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RegisterUserDTO } from 'src/app/common/models/registerUserDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) { }

  registerUser(user: RegisterUserDTO) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/register', user, {withCredentials: false});
  }

  getUserData() {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'User/GetUser', {withCredentials: false});
  }
}
