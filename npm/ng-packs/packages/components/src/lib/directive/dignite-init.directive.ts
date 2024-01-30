import { AfterViewInit, Directive, ElementRef, EventEmitter, Output } from '@angular/core';

@Directive({
  selector: '[digniteInit]'
})
export class DigniteInitDirective implements AfterViewInit{
  @Output('digniteInit') readonly init = new EventEmitter<ElementRef<any>>();
  constructor(private elRef: ElementRef) {}
  ngAfterViewInit() {
    this.init.emit(this.elRef);
  }

}
