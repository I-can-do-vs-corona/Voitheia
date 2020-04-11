import { Component, OnInit, HostListener, NgModule } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isCollapsed = true;

  unstickyHeaderHeight = 100;
  offset = 5;
  scrollFactor = 1.5;

  constructor(private _authService: AuthService) { 
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

  public hamburgerMenuToggle(){
    this.isCollapsed = !this.isCollapsed;
    let ham = document.getElementById("hamburger");
    let content = document.getElementById("navbarSupportedContent");
     if(this.isCollapsed){
        ham.classList.add("collapsed");
     }else {
       ham.classList.remove("collapsed");
    }
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    this.navbarCollapse();
  }
}