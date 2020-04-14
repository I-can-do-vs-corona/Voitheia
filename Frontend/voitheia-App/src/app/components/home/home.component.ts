import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/common/shared/services/auth.service';

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
