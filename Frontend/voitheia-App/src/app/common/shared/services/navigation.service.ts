import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  constructor(private _router: Router) { }

  navigateTo(route: string) {
    this._router.navigate(['/' + route]);
  }
}
