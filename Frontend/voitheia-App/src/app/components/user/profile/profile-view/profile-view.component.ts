import { Component, OnInit } from '@angular/core';
import { UserService } from '../../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.scss']
})
export class ProfileViewComponent implements OnInit {

  constructor(private _userService: UserService, private _utilitiesService:UtilitiesService) { }

  ngOnInit(): void {
    this._userService.getUserData().subscribe(
      data => {
        debugger;
      },
      err => {
        this._utilitiesService.handleError(err);
      }
    );
  }

}
