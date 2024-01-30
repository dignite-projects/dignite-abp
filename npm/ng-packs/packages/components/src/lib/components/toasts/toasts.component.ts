import { Component } from '@angular/core';
import { ToastService, TypeClass } from '../../services';

@Component({
  selector: 'dignite-toasts',
  // standalone: true,
  // templateUrl:'./toasts.component.html',
  template: `
    <ng-container *ngFor="let toast of _toastService.toasts">
      <ngb-toast
        [class]="TypeClass[toast.type] + ' ' + toast.classname "
        [autohide]="true"
        [delay]="toast.type==='loading'?5000000000:toast.delay || 5000"
        (hidden)="_toastService.remove(toast)"
      >
        <ng-container *ngIf="toast.template; else elseTemplate">
          <ng-template [ngTemplateOutlet]="toast.template"></ng-template>
        </ng-container>
        <ng-template #elseTemplate>
          <i class="fa fa-spinner  fa-spin" *ngIf="toast.type==='loading'" aria-hidden="true"></i>
          {{ toast.content }}
        </ng-template>
      </ngb-toast>
    </ng-container>
  `,
  styles: [
    `
      ::ng-deep .dignite-toast .toast {
        max-width: 350px;
        width: auto;
      }
    `,
  ],
  host: {
    class: 'toast-container position-fixed top-0 start-50 p-3 translate-middle-x  dignite-toast',
    style: 'z-index: 1200',
  },
})
export class ToastsComponent {
  constructor(public _toastService: ToastService) { }
  /**toast状态 */
  TypeClass = TypeClass;
}
