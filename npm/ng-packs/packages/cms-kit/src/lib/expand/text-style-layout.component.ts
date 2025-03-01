/* eslint-disable @angular-eslint/component-selector */
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-text-style-layout',
  template: `
      <div class="py-md-2 pt-md-0 mb-3" [class]=" type==='flex'?'d-flex  align-content-center':''">
        <label class="form-label me-2" *ngIf="key"> {{ key | abpLocalization }}: </label>
        <div class="real-text fs-5 "><strong [innerHTML]="value"></strong></div>
      </div>
  `,
  styles: [`

    `],
})
export class TextStyleLayoutComponent {
  @Input() key: any;
  @Input() value: any;
  @Input() col: any;
  @Input() type: any='';
}
