import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from './utilities.service';
import { CredentialsDTO } from '../../models/credentialsDTO';
import { NavigationService } from './navigation.service';
import * as moment from 'moment';
import { JwtDTO } from '../../models/JwtDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService, private _navigationService: NavigationService) {}

  getExpiration(): moment.Moment {
    return moment.utc(localStorage.getItem("validUntil"));
  }

  setToken(token: string){
    localStorage.setItem('token', token);
  }

  getToken(): string{
    return localStorage.getItem('token');
  }

  isLoggedIn():boolean{
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

  login(userCredentials: CredentialsDTO) {
    this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/login',  userCredentials, {withCredentials: false}).subscribe(
      data => {
        this.handleSuccessfullLogin(data as JwtDTO);
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

  handleSuccessfullLogin(authResult: JwtDTO){
    this.setSession(authResult);
    this._navigationService.navigateTo('user/profile');
  }

  private setSession(authResult: JwtDTO) {
    localStorage.setItem('token', authResult.token);
    localStorage.setItem("validUntil", authResult.validUntil.toString());
  } 

  private delSession() {
    localStorage.removeItem('token');
    localStorage.removeItem('validUntil');
  }
}
