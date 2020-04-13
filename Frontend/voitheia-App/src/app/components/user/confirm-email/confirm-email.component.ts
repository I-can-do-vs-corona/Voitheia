import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfirmEmailDTO } from 'src/app/common/models/confirmEmailDTO';
import { UserService } from '../user.service';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  processing = true;
  confirmSuccess = false;

  confirmEmail: ConfirmEmailDTO;

  constructor(private _route: ActivatedRoute, private _userService: UserService, private _utilitiesService: UtilitiesService) { }

  ngOnInit(): void {
    this.confirmEmail = new ConfirmEmailDTO();
    
    if (this._route.snapshot.queryParamMap.get('token') !== null) {
      this.confirmEmail.emailToken = this._route.snapshot.queryParamMap.get('token');
    }

    if (this._route.snapshot.queryParamMap.get('email') !== null) {
      this.confirmEmail.email = this._route.snapshot.queryParamMap.get('email');
    }

    if(this.confirmEmail.email !== "" && this.confirmEmail.emailToken !== ""){
      this._userService.confirmEmail(this.confirmEmail).subscribe(
        data => {
          debugger;
          this.processing = false;
          this.confirmSuccess = true;
        },
        err => {
          debugger;
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
