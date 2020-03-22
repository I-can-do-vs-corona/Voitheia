import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/user/register/register.component';
import { LoginComponent } from './components/user/login/login.component';
import { TermsComponent } from './components/terms/terms/terms.component';
import { HomeComponent } from './components/home/home.component';
import { RequestViewComponent } from './components/request/request-view/request-view.component';
import { RequestListComponent } from './components/request/request-list/request-list.component';
import { RequestFormComponent } from './components/request/request-form/request-form.component';
import { RequestFormSuccessComponent } from './components/request/request-form/request-form-success/request-form-success.component';
import { MyRequestsListComponent } from './components/my-requests/my-requests-list/my-requests-list.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'terms',
    component: TermsComponent,
    data: {title: 'Terms'}
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {title: 'Login'}
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {title: 'Register'}
  },
  {
    path: 'home',
    component: HomeComponent,
    data: {title: 'Home'}
  },
  {
    path: 'request',
    children: [
      {
        path: 'list',
        component: RequestListComponent,
        data: {title: 'Request.List'},
      },
      {
        path: 'create',
        component: RequestFormComponent,
        data: {title: 'Request.Form'}
      }
    ]
  },
  {
    path: 'my-requests',
    children: [
      {
        path: 'list',
        component: MyRequestsListComponent,
        data: {title: 'Request.List'},
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
