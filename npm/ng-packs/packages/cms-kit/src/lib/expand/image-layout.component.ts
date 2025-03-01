/* eslint-disable @angular-eslint/component-selector */
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-layout',
  template: `
    <div class="mb-3" [class]="'col' + (col ? '-' + col : '')">
      <div class=" py-md-2 pt-0"  [class]="type==='flex'?'d-flex  align-content-center':''">
        <label class="form-label fs-5"> {{ key | abpLocalization }}: </label>
        <app-image-preview [src]="value"></app-image-preview>
      </div>
    </div>
  `,
  styles: [
    `
      app-text-style-layout {
        width: auto;
      }
    `,
  ],
})

export class ImageLayoutComponent {
  @Input() key: any;
  @Input() value: any;
  @Input() col: any;
  @Input() type: any;
}
