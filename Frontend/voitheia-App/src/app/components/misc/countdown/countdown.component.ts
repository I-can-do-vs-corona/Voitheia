import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit {

  text:any = {
    Year: 'Jahre',
    Month: 'Monate',
    Weeks: "Wochen",
    Days: "Tage",
    Hours: "Stunden",
    Minutes: "Minuten",
    Seconds: "Sekunden",
    MilliSeconds: "Millisekunden"
  };

  goLive: Date;

  constructor(private _utilitiesService: UtilitiesService, private _navigationService: NavigationService, private _authService: AuthService) { }

  ngOnInit(): void {
    this.goLive = environment.goLiveDate;

    if(this._utilitiesService.isLive()){
      this._navigationService.navigateTo("home");
    }
  }

  registrationOpen(){
    return this._utilitiesService.isRegistrationOpen();
  }
}
