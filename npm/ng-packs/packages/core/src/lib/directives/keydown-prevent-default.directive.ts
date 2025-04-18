/* eslint-disable @angular-eslint/directive-selector */
import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[KeydownPreventDefault]',
  standalone: true,
})
export class KeydownPreventDefaultDirective {

  @HostListener('keydown.enter', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    event.preventDefault();
  }
}
