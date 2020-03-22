import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { User } from 'src/app/common/models/User';
import { LoginCredentials } from 'src/app/common/models/loginCredentials';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}

  requestLogin(user: User) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/login',  new LoginCredentials(user.email, user.password), {withCredentials: false});
  }

}
