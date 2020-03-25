import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/common/models/user';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { faPaperPlane, faUserPlus } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/common/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: User;
  faPaperPlane = faPaperPlane;
  faUserPlus = faUserPlus;
  
  constructor(private _authService: AuthService, private _navigationService: NavigationService) {
    this.user = new User();
  }

  ngOnInit(): void {
      
  }

  public registerUser(){
    this._navigationService.navigateTo('register');
  }

  public send(){    
    this._authService.requestLogin(this.user).subscribe(
      data => {
        this._authService.setToken(data["token"]);
        this._navigationService.navigateTo('home');
      },
      err => {
        console.log(err);
        if(err["status"] === 400){
          alert("Login Error, please check email and password!");
        }
        else{
          alert("Error");
        }
      }
    );
  }
}
