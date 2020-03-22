import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/common/models/user';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { RegisterService } from '../register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  user: User;
  faPaperPlane = faPaperPlane;
  
  constructor(private _registerService: RegisterService, private _navigationService: NavigationService) {
    this.user = new User();
  }

  ngOnInit(): void {
  }

  public cancelRegister(){
    this._navigationService.navigateTo('login');

  }

  public send(){    
    this._registerService.registerUser(this.user).subscribe(
      data => {
        console.log(data);
        
        //TODO: Handle Login Success
        
        //this._navigationService.navigateTo("request/create/success")
      },
      err => {
        alert("error");
      }
    );
  }
}