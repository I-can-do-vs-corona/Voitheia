import { Component, OnInit } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { CredentialsDTO } from 'src/app/common/models/credentialsDTO';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: CredentialsDTO;
  
  constructor(private _authService: AuthService) {
    this.user = new CredentialsDTO();
  }

  ngOnInit(): void {
      
  }

  public login(){ 
    this._authService.login(this.user);
  }
}
