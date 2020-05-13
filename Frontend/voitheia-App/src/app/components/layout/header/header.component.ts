import { Component, OnInit, HostListener, NgModule } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';
import { AuthService } from 'src/app/common/shared/services/auth.service';
import { TranslateService } from '@ngx-translate/core';


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

  constructor(private _authService: AuthService, private _translateService: TranslateService) { 
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

  public changeLanguage(languageCode: string) {
    // the lang to use, if the lang isn't available, it will use the current loader to get them
    if (languageCode === 'de' || languageCode === 'en') {
      this._translateService.use(languageCode);
    }
  }

  public getCurrentLanguage() {
    return this._translateService.currentLang;
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    this.navbarCollapse();
  }
}