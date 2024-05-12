import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UpdateListService {

  public updateListEvent: EventEmitter<any> = new EventEmitter();

  constructor() { }

  updateList() {
    this.updateListEvent.emit();
  }
}
