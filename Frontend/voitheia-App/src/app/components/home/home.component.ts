import { Component, OnInit } from '@angular/core';
import { NavigationService } from 'src/app/common/shared/services/navigation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(public _navigationService: NavigationService) { }

  ngOnInit(): void {
  }

}
