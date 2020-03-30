import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/components/user/token.interceptor';
import { FormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './common/shared/shared/shared.module';
import { MaterialModule } from './common/modules/material/material.module';
import { CustomFontAwesomeModule } from './common/modules/custom-font-awesome/custom-font-awesome.module';
import { HomeComponent } from './components/home/home.component';
import { RequestFormComponent } from './components/request/request-form/request-form.component';
import { RequestViewComponent } from './components/request/request-view/request-view.component';
import { TermsComponent } from './components/terms/terms/terms.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { RequestListComponent } from './components/request/request-list/request-list.component';
import { KeysPipe } from './common/helper/pipes/keys.pipe';
import { MyRequestsListComponent } from './components/my-requests/my-requests-list/my-requests-list.component';
import { MyRequestsViewComponent } from './components/my-requests/my-requests-view/my-requests-view.component';
import { DistancePipe } from './common/helper/pipes/distance.pipe';

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
    DistancePipe
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
    SharedModule.forRoot(),
    MaterialModule,
    CustomFontAwesomeModule,
    FormsModule
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
export class AppModule { }

// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}