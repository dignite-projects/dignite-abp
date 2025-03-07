import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
/**
 * 子页面更新父级列表服务
 * 
 * 父页面：
 * 
    private _UpdateListService=inject(UpdateListService)

    this._UpdateListService.updateListEvent.subscribe(() => {
      this.list.get()
    });
 * 
 * 子页面：
 * 
    private _UpdateListService=inject(UpdateListService)
    this._UpdateListService.updateList();
 */

@Injectable({
  providedIn: 'root',
})
export class UpdateListService {
  public updateListEvent: EventEmitter<any> = new EventEmitter();


  updateList() {
    this.updateListEvent.emit();
  }

  /**
   * 使用发起数据
   * this.dataSubject.next(res);
   * 
   * 接收数据产生回调
    this._UpdateLogoService.userInfo$.subscribe((res) => {
      this.getData();
    });
   */

  private dataSubject = new BehaviorSubject<any>(null);
  data$ = this.dataSubject.asObservable();
  updatedata(newdata: any) {
    this.dataSubject.next(newdata);
  }
}
