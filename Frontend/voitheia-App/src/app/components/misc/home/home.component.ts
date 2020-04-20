import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private _authService: AuthService, private _utilitiesService: UtilitiesService, private _navigationService: NavigationService) { }

  ngOnInit(): void {
    if(!this._utilitiesService.isLive()){
      this._navigationService.navigateTo("countdown");
    }
  }

  public isLoggedIn(){
    return this._authService.isLoggedIn();
  }
}
