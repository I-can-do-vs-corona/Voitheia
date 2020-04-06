import { Component, OnInit } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { DialogService } from 'src/app/common/shared/services/dialog/dialog.service';
import { DialogIconTypeEnum } from 'src/app/common/helper/enums';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
  }

  public isLoggedIn(){
    return this._authService.isLoggedIn();
  }
}
