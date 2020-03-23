import { Component, OnInit, HostListener } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  stickyHeaderHeight = 70;
  unstickyHeaderHeight = 100;
  offset = 5;
  scrollFactor = 1.5;

  constructor(public _navigationService: NavigationService) { }

  ngOnInit(): void {
    
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    if (window.pageYOffset > (this.unstickyHeaderHeight + this.offset) && window.pageYOffset < (this.unstickyHeaderHeight + this.offset + (this.stickyHeaderHeight * this.scrollFactor))){
      var calculatedTop = (window.pageYOffset - (this.unstickyHeaderHeight + this.offset + (this.stickyHeaderHeight * this.scrollFactor)));

      let headerElement = document.getElementById('header');
      headerElement.classList.add('sticky');
      headerElement.setAttribute("style", "top:" + calculatedTop + "px");

      let tableHeaders = document.getElementsByClassName('mat-table-sticky');
      for (let i = 0; i < tableHeaders.length; i++) {
        const element = tableHeaders[i] as HTMLElement;
        element.style.top = (this.stickyHeaderHeight + calculatedTop) + "px";
      }
    }
    else if (window.pageYOffset >= (this.unstickyHeaderHeight + this.offset + (this.stickyHeaderHeight * this.scrollFactor))) {
      let headerElement = document.getElementById('header');
      headerElement.classList.add('sticky');
      headerElement.removeAttribute("style");

      let tableHeaders = document.getElementsByClassName('mat-table-sticky');
      for (let i = 0; i < tableHeaders.length; i++) {
        const element = tableHeaders[i] as HTMLElement;
        element.style.top = this.stickyHeaderHeight + "px";
      }
    } else {
      let headerElement = document.getElementById('header');
      headerElement.classList.remove('sticky');
      headerElement.removeAttribute("style");

      let tableHeaders = document.getElementsByClassName('mat-table-sticky');
      for (let i = 0; i < tableHeaders.length; i++) {
        const element = tableHeaders[i] as HTMLElement;
        element.style.top = "0px";
      }
    }
  }

}