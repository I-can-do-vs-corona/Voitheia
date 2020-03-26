import { Injectable } from '@angular/core';
import { User } from 'src/app/common/models/User';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) { }

  registerUser(user: User) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/register', user, {withCredentials: false});
  }
}
