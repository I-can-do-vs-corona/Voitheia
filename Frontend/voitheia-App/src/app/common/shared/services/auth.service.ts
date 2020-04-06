import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from './utilities.service';
import { User } from '../../models/User';
import { LoginCredentials } from '../../models/loginCredentials';
import { NavigationService } from './navigation.service';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService, private _navigationService: NavigationService) {}

  getExpiration() {
    return moment.utc(localStorage.getItem("validUntil"));
  }

  setToken(token: string){
    localStorage.setItem('token', token);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  isLoggedIn() {
    var loggedIn;

    if (this.getToken() === null) {
      loggedIn = false;
    } else{
      loggedIn = moment().isSameOrBefore(this.getExpiration());
    }

    if(!loggedIn){
      this.delSession();
    }

    return loggedIn;
  }

  logout() {
    this.delSession();
    this._navigationService.navigateTo('home');
  }

  login(user: User) {
    this._httpClient.post(this._utilitiesService.getAPIUrl() + 'api/user/login',  new LoginCredentials(user.email, user.password), {withCredentials: false}).subscribe(
      data => {
        this.setSession(data);
        this._navigationService.navigateTo('home');
      },
      err => {
        console.log(err);
        if(err["status"] === 400){
          this._utilitiesService.handleError("Login Error, please check email and password!");
        }
        else{
          this._utilitiesService.handleError("Error");
        }
      }
    );
  }

  private delSession() {
    localStorage.removeItem('token');
    localStorage.removeItem('validUntil');
  }

  private setSession(authResult) {
    localStorage.setItem('token', authResult.token);
    localStorage.setItem("validUntil", authResult.validUntil);
  } 
}
