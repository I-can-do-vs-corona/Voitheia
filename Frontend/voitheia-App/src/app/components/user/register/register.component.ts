import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { JwtDTO } from 'src/app/common/models/JwtDTO';
import { RegisterUserDTO } from 'src/app/common/models/registerUserDTO';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  user: RegisterUserDTO;
  termsChecked = false;
  
  constructor(private _userService: UserService, private _authService: AuthService, private _utilitiesService: UtilitiesService) {
    this.user = new RegisterUserDTO();
  }

  ngOnInit(): void {
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