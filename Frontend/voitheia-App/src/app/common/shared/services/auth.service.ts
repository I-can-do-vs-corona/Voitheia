import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from './utilities.service';
import { User } from '../../models/User';
import { LoginCredentials } from '../../models/loginCredentials';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _route: Router, private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) {}


  setToken(token: string){
    localStorage.setItem('token', token);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  isLoggedIn() {
    if (this.getToken() === null) {
      return false;
    }
    
    return true;
  }

  logout() {
    this.delSession();
  }

  private delSession() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiresAt');
    localStorage.removeItem('userName');
  }

  requestLogin(user: User) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/login',  new LoginCredentials(user.email, user.password), {withCredentials: false});
  }

  registerUser(user: User) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/register', user, {withCredentials: false});
  }
}
