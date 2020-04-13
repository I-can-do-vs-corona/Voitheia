import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/user/register/register.component';
import { LoginComponent } from './components/user/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RequestListComponent } from './components/request/request-list/request-list.component';
import { RequestFormComponent } from './components/request/request-form/request-form.component';
import { MyRequestsListComponent } from './components/my-requests/my-requests-list/my-requests-list.component';
import { AuthGuard } from './common/guards/auth.guard';
import { TermsComponent } from './components/terms/terms/terms.component';
import { PrivacyComponent } from './components/terms/privacy/privacy.component';
import { AboutUsComponent } from './components/about/about-us/about-us.component';
import { IdeaComponent } from './components/about/idea/idea.component';
import { PageNotFoundComponent } from './components/misc/page-not-found/page-not-found.component';
import { ImprintComponent } from './components/terms/imprint/imprint.component';
import { ProfileViewComponent } from './components/user/profile/profile-view/profile-view.component';
import { ProfileEditComponent } from './components/user/profile/profile-edit/profile-edit.component';
import { CountdownComponent } from './components/misc/countdown/countdown.component';
import { ConfirmEmailComponent } from './components/user/confirm-email/confirm-email.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent,
    data: {title: 'Home'}
  },
  {
    path: 'request/create',
    component: RequestFormComponent,
    data: {title: 'Request.Create'}
  },
  {
    path: '', // authenticated Area
    canActivate: [AuthGuard],
    children: [
      {
        path: 'my-requests',
        children: [
          {
            path: 'list',
            component: MyRequestsListComponent,
            data: {title: 'MyRequest.List'}
          }
        ]
      },
      {
        path: 'request',
        children: [
          {
            path: 'list',
            component: RequestListComponent,
            data: {title: 'Request.List'}
          }
        ]
      },
      {
        path: 'user',
        children: [
          {
            path: 'profile',
            component: ProfileViewComponent,
            data: {title: 'Profile.View'}
          },
          {
            path: 'profile/edit',
            component: ProfileEditComponent,
            data: {title: 'Profile.Edit'}
          }
        ]
      }
    ]
  },
  {
    path: 'user',
    children: [
      {
        path: 'login',
        component: LoginComponent,
        data: {title: 'User.Login'}
      },
      {
        path: 'register',
        component: RegisterComponent,
        data: {title: 'User.Register'}
      },
      {
        path: 'confirm-email',
        component: ConfirmEmailComponent,
        data: {title: 'User.ConfirmEmail'}
      }
    ]
  },
  {
    path: 'terms',
    children: [
      {
        path: 'terms-conditions',
        component: TermsComponent,
        data: {title: 'Terms.TermsConditions'}
      },
      {
        path: 'privacy-policy',
        component: PrivacyComponent,
        data: {title: 'Terms.PrivacyPolicy'}
      },
      {
        path: 'imprint',
        component: ImprintComponent,
        data: {title: 'Terms.Imprint'}
      }
    ]
  },
  {
    path: 'about',
    children: [
      {
        path: 'about-us',
        component: AboutUsComponent,
        data: {title: 'About.AboutUs'}
      },
      {
        path: 'idea',
        component: IdeaComponent,
        data: {title: 'About.Idea'}
      }
    ]
  },
  { 
    path: 'countdown',
    component: CountdownComponent,
    data: {title: 'Misc.Countdown'} },
  { 
    path: '**',
    component: PageNotFoundComponent,
    data: {title: 'Misc.PageNotFound'}
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
