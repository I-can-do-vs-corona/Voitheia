import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { filter, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UtilitiesService } from './common/shared/services/utilities.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Voitheia';

  constructor(
    private _utilitiesService: UtilitiesService,
    private _translate: TranslateService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _titleService: Title) {
      _translate.addLangs(['de']); //, 'en', 'sv'
      _translate.setDefaultLang('de');
      _translate.use("de");

      registerLocaleData(localeDe, 'de');

      // Setting the Title correct! Loading data from rout elements
      this._router.events.pipe(
        filter(event => event instanceof NavigationEnd)
      ).pipe(
        map(() => {
          let child = this._activatedRoute.firstChild;
          while (child) {
            if (child.firstChild) {
              child = child.firstChild;
            } else if (child.snapshot.data && child.snapshot.data['title']) {
              return child.snapshot.data['title'];
            } else {
              return null;
            }
          }
          return null;
        })
      ).subscribe((title) => {
        if (title != null && title !== '' && title !== undefined){
          // _translate.stream is like .get but will watch on language changes.
          this._translate.stream(['General.PageTitles.Default', 'General.PageTitles.' + title]).subscribe((res: string) => {
            if (res['General.PageTitles.Default'] !== 'General.PageTitles.Default' && res['General.PageTitles.Default'] !== '') {
              let composedTitle = res['General.PageTitles.Default'];
              if (res['General.PageTitles.' + title] !== 'General.PageTitles.' + title && res['General.PageTitles.' + title] !== '') {
                composedTitle += ' - ' + res['General.PageTitles.' + title];
              }
              this._titleService.setTitle(composedTitle);
            }
          });
        }
      });
    }

  isProduction(): boolean{
    return environment.production;
  }
}