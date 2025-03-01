/* eslint-disable @angular-eslint/component-selector */
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-ckeditor-layout',
  template: `
    <div class=" py-md-2 mb-3">
      <label class="form-label "> {{ key | abpLocalization }}: </label>
      <div class="real-text fs-5 fw-semibold">
        <div [innerHTML]="value"></div>
      </div>
    </div>
  `,
  styles: [``],
})

export class CkeditorLayoutComponent {
  @Input() key: any;
  @Input() value: any;
  @Input() col: any;
}
