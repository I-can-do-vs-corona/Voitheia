import { Component, OnInit } from '@angular/core';
import { ConfirmEmailDTO } from 'src/app/common/models/confirmEmailDTO';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  processing = true;
  confirmSuccess = false;

  confirmEmail: ConfirmEmailDTO;

  constructor(private _navigationService: NavigationService, private _userService: UserService, private _utilitiesService: UtilitiesService) { }

  ngOnInit(): void {
    this.confirmEmail = new ConfirmEmailDTO();
    
    this.confirmEmail.emailToken = this._navigationService.getRouteParameter("token");

    this.confirmEmail.email = this._navigationService.getRouteParameter("email");

    if(this.confirmEmail.email !== "" && this.confirmEmail.emailToken !== ""){
      this._userService.confirmEmail(this.confirmEmail).subscribe(
        data => {
          this.processing = false;
          this.confirmSuccess = true;
        },
        err => {
          this._utilitiesService.handleError(err);
          this.processing = false;
          this.confirmSuccess = false;
        }
      );
    } else{
      this.processing = false;
      this.confirmSuccess = false;
    }
  }

}
