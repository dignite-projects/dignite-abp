import { Component, Input } from '@angular/core';

@Component({
  selector: 'dignite-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss']
})
export class TabsComponent {
  @Input() class:string
}
