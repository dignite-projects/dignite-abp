import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'dignite-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent {
  constructor(
    public _location: Location,
  ) { }
  /**
   * 是否允许返回上一页
   * 默认不允许
   */
  @Input() back:boolean=false
  @Input() title:string='页面标题'
  /**返回上一页 */
  goBack(){
    this._location.back();
  }
}
