import { Component, ContentChild, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'dignite-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent {
  @Input() isback:boolean=false
  @Input() title:string='页面标题'
  
  @ContentChild('pageHeader', { static: false }) pageHeader?: TemplateRef<any>;

  @ContentChild('pageBody', { static: false }) pageBody?: TemplateRef<any>;
}
