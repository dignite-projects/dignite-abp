import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

export interface backBase {
  //检查的链接，如果链接中包含该字符串，则返回上一页，否则跳转到Url页
  url?: string;
  //链接补充，用于在url后面补充其余链接
  replenish?: string;
}

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
