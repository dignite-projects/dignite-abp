import { ApplicationRef, ComponentFactoryResolver, Injectable, Injector, TemplateRef } from '@angular/core';
import { ToastsComponent } from '../components';

export interface Toast {
  /**有值则使用模板，无值测使用文本,不可与content，loading同时使用 */
  template?: TemplateRef<any>;
  /**提示框内容，不与 template 同时使用*/
  content?: string,
  /**提示类型  
   * 常规白底：''
   * 标准:primary       bg-primary text-light
   * 黑:dark            bg-dark    text-light
   * 成功:success       bg-success text-light
   * 警告:warning       bg-warning text-dark
   * 危险:danger        bg-danger  text-light
   * 加载:loading       bg-primary text-dark
  */
  type?: string;
  /** */
  /**附加类 提示位置可通过附加类调整*/
  classname?: string;
  /**提示延迟，既提示存在几秒 */
  delay?: number;
  /**时间戳 */
  date?: Date | number | string,
  /**结束 */
  success?: Function
}

export enum TypeClass {
  primary = 'bg-primary text-light',
  dark = 'bg-dark text-light',
  success = 'bg-success text-light',
  warning = 'bg-warning text-light',
  danger = 'bg-danger text-light',
  loading = 'bg-primary text-light',
}


@Injectable({
  providedIn: 'root'
})
export class ToastService {
  
  constructor(
    private injector: Injector,
    private applicationRef: ApplicationRef,
    private componentFactoryResolver: ComponentFactoryResolver
  ) { }
  // ToastsContainerComponent = ToastsComponent

  /**全局提示列表 */
  toasts: Toast[] = [];

  /**弹窗服务标签引入内容 */
  popup: any

  /**弹窗服务组件 */
  popupComponentRef: any
  /**是否创建页面toast节点 */
  isCreateElement:boolean=false

  /**提示显示 */
  show(toast: Toast) {
    return new Promise(async(resolve, reject) => {
      if(!this.isCreateElement) this.showAsComponent()
      
      toast.date = new Date().getTime()
      toast.type = toast.type ? toast.type : 'success'
      toast.classname = toast.classname || ''
      this.toasts.push(toast);
      if(toast.type=='loading') return  resolve(toast)
      setTimeout(() => {
        resolve(toast)
      }, toast.delay || 5000)
    })
  }

  async remove(toast?: Toast | any) {
    let toastVal = await toast
    this.toasts = this.toasts.filter((t) => t.date !== toastVal.date);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }



  /**创建页面元素组件 */
  showAsComponent() {
    // return new Promise((resolve, rejects) => {
      this.isCreateElement=true
      // Create element
      const popup = document.createElement('dignite-toasts');
      // Create the component and wire it up with the element
      const factory = this.componentFactoryResolver.resolveComponentFactory(ToastsComponent);
      const popupComponentRef = factory.create(this.injector, [], popup);
      // Attach to the view so that the change detector knows to run
      this.applicationRef.attachView(popupComponentRef.hostView);
      // Add to the DOM
      document.body.appendChild(popup);
  }

}
