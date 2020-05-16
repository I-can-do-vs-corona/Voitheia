import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  constructor(private _router: Router, private _activeRoute: ActivatedRoute) { }

  navigateTo(route: string) {
    this._router.navigate(['/' + route]);
    window.scrollTo(0,0);
  }

  routeParameterIsSet(parameterName: string): boolean{
    if(typeof(this._activeRoute.snapshot.queryParamMap.get(parameterName)) === 'undefined' || this._activeRoute.snapshot.queryParamMap.get(parameterName) === null){
      return false;
    }

    return true;
  }

  getRouteParameter(parameterName: string): string{
    if(this.routeParameterIsSet(parameterName)){
      return this._activeRoute.snapshot.queryParamMap.get(parameterName);
    }
    return "";
  }
}
