import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { backBase } from './models';


/**
 * 用于返回上一页的服务
 */
@Injectable({
  providedIn: 'root',
})
export class LocationBackService {
  constructor(public _location: Location, private router: Router) {}
  backTo(input?: backBase) {
    const { url, replenish } = input;
    const referrer = document.referrer;
    const targetUrl = url;
    //如果referrer中包含targetUrl，则返回上一页，否则跳转到targetUrl页
    if (referrer && referrer.includes(targetUrl) && !referrer.includes(targetUrl + replenish)) {
      this.back();
    } else {
      this.router.navigate([targetUrl]);
    }
  }

  back() {
    this._location.back();
  }
}
