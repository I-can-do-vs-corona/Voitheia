import { Component, OnInit, HostListener } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { faSignInAlt, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  faSignInAlt = faSignInAlt;
  faSignOutAlt = faSignOutAlt;
  unstickyHeaderHeight = 100;
  offset = 5;
  scrollFactor = 1.5;
  public isMenuCollapsed = true;

  constructor(public _navigationService: NavigationService, private _authService: AuthService) { 
  }
  ngOnInit(): void {
    
  }

  public isLoggedIn(){
    
    return this._authService.isLoggedIn();
  }

  public logout(){
    this._authService.logout();
  }

  public navbarCollapse(){
    console.log("collapsing navbar");
    if (window.pageYOffset > (this.unstickyHeaderHeight + this.offset)){
      let headerElement = document.getElementById('mainNav');
      headerElement.classList.add('navbar-scrolled');

    }
    else {
      let headerElement = document.getElementById('mainNav');
      headerElement.classList.remove('navbar-scrolled');
    }
  }

 


  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    this.navbarCollapse();
  }
}