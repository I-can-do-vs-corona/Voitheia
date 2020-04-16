import { Component, OnInit } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { CredentialsDTO } from 'src/app/common/models/credentialsDTO';
import { environment } from 'src/environments/environment';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: CredentialsDTO;

  reCaptchaKey = '';
  reCaptchaValid = false;
  
  constructor(private _authService: AuthService, private _translateService: TranslateService) {
    this.user = new CredentialsDTO();
  }

  ngOnInit(): void {
    this.reCaptchaKey = environment.reCaptchaKey;
    if (!this.reCaptchaKey || this.reCaptchaKey === '') {
      this.reCaptchaValid = true;
    }
      
  }

  public login(){ 
    this._authService.login(this.user);
  }

  handleCorrectCaptcha(event) {
    this.reCaptchaValid = true;
  }

  handleExpiredCaptcha() {
    this.reCaptchaValid = false;
  }

  getreCaptchaLanguage() {
    if (this._translateService.currentLang === 'de') {
      return 'de';
    } else if (this._translateService.currentLang === 'se') {
      return 'sv';
    }
    return 'en';
  }
}
