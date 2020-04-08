import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';
registerLocaleData(localeDe);

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/components/user/token.interceptor';
import { FormsModule }   from '@angular/forms';
import { CollapseModule, BsDropdownModule} from 'ngx-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomMaterialModule } from './common/modules/material/material.module';
import { HomeComponent } from './components/home/home.component';
import { RequestFormComponent } from './components/request/request-form/request-form.component';
import { RequestViewComponent } from './components/request/request-view/request-view.component';
import { TermsComponent } from './components/terms/terms/terms.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { RequestListComponent } from './components/request/request-list/request-list.component';
import { KeysPipe } from './common/helper/pipes/keys.pipe';
import { DistancePipe } from './common/helper/pipes/distance.pipe';
import { MyRequestsListComponent } from './components/my-requests/my-requests-list/my-requests-list.component';
import { MyRequestsViewComponent } from './components/my-requests/my-requests-view/my-requests-view.component';
import { DialogComponent } from './common/shared/services/dialog/dialog.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PrivacyComponent } from './components/terms/privacy/privacy.component';
import { IdeaComponent } from './components/about/idea/idea.component';
import { AboutUsComponent } from './components/about/about-us/about-us.component';
import { PageNotFoundComponent } from './components/misc/page-not-found/page-not-found.component';
import { ImprintComponent } from './components/terms/imprint/imprint.component';
import { CustomBootstrapModule } from './common/modules/bootstrap/bootstrap.module';


@NgModule({
  declarations: [
    AppComponent,
    KeysPipe,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    FooterComponent,
    HomeComponent,
    RequestFormComponent,
    RequestViewComponent,
    TermsComponent,
    HeaderComponent,
    RequestListComponent,
    MyRequestsListComponent,
    MyRequestsViewComponent,
    DistancePipe,
    DialogComponent,
    PrivacyComponent,
    IdeaComponent,
    AboutUsComponent,
    PageNotFoundComponent,
    ImprintComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    FontAwesomeModule,
    CustomMaterialModule,
    //CustomBootstrapModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}