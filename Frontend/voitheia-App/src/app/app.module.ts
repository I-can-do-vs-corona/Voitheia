import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';
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
import { RequestFormSuccessComponent } from './components/request/request-form/request-form-success/request-form-success.component';

@NgModule({
  declarations: [
    AppComponent,
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
    RequestFormSuccessComponent
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
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}