import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'voitheia-App';

  constructor(
    translate: TranslateService) {
      translate.addLangs(['de']); //, 'en', 'sv'
      translate.setDefaultLang('de');

      registerLocaleData(localeDe, 'de');
      }
}
