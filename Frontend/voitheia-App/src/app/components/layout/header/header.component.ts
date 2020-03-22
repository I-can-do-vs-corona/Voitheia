import { Component, OnInit, HostListener } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(public _navigationService: NavigationService) { }

  ngOnInit(): void {
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
     if (window.pageYOffset > 120) {
       let headerElement = document.getElementById('header');
       headerElement.classList.add('sticky');
       headerElement.classList.add('sticky-top');

       let pageContentElement = document.getElementById('page-content');
       pageContentElement.classList.add('marginTop');
     } else {
       let headerElement = document.getElementById('header');
       headerElement.classList.remove('sticky'); 
       headerElement.classList.remove('sticky-top'); 

       let pageContentElement = document.getElementById('page-content');
       pageContentElement.classList.remove('marginTop');
     }
  }

}