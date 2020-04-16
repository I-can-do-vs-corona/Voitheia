import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { JwtDTO } from 'src/app/common/models/JwtDTO';
import { RegisterUserDTO } from 'src/app/common/models/registerUserDTO';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  user: RegisterUserDTO;
  termsChecked = false;
  repeatPassword = "";
  hidePW = true;
  passwordRegEx = '';
  
  constructor(private _userService: UserService, private _authService: AuthService, private _utilitiesService: UtilitiesService, private _navigationService: NavigationService) {
    this.user = new RegisterUserDTO();
    this.passwordRegEx = _utilitiesService.passwordRegEx;
  }

  ngOnInit(): void {
    if(!this._utilitiesService.isRegistrationOpen()){
      this._navigationService.navigateTo("countdown");
    }
  }

  public send(){    
    this._userService.registerUser(this.user).subscribe(
      data => {
        this._authService.handleSuccessfullLogin(data as JwtDTO);
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }
}