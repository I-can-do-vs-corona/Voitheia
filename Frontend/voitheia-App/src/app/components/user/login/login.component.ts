import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/common/models/user';
import { LoginService } from '../login.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: User;
  token: string;
  faPaperPlane = faPaperPlane;
  
  constructor(private _loginService: LoginService, private _navigationService: NavigationService) {
    this.user = new User();
    this.token = "";
  }

  ngOnInit(): void {
  }

  public registerUser(){
    this._navigationService.navigateTo('register');
  }

  public send(){    
    this._loginService.requestLogin(this.user).subscribe(
      data => {
        console.log(data);
        this.token = data["token"];
        //TODO: Handle Login Success
      },
      err => {
        console.log(err);
        alert("error");
      }
    );
  }
}
