/* eslint-disable @angular-eslint/component-selector */
import { PageModule } from '@abp/ng.components/page';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output, TemplateRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-image-preview',
  template: `
    <div
      [id]="id"
      *ngIf="_src"
      class="image-preview imagehove border  position-relative  m-1 overflow-hidden "
      [style]="{'width': width+'px','height': height+'px',borderRadius:rounded}" [ngClass]="{'rounded-4': !rounded,'bg-black':bgBlack}"
    >
      <img [src]="_src" alt="" class="w-100 h-100 object-fit-cover" />
      <input
        type="checkbox"
        class="form-check-input  position-absolute top-0 end-0  m-1"
        *ngIf="checkChange.observed"
        (change)="checkImage($event)"
        style="z-index: 10;"
      />
      <div
        class="w-100 h-100  text-white position-absolute top-0 start-0"
        style="background-color: #00000050;"
        *ngIf="preview || deleteChange.observed"
      >
        <i
          type="button"
          class="fa fa-eye  p-2"
          *ngIf="preview"
          (click.stop)="OpenPreviewImage(content)"
        ></i>
        <i
          type="button"
          class="fa fa-times p-2"
          *ngIf="deleteChange.observed"
          (click.stop)="deletecoverPics()"
        ></i>
      </div>
    </div>

    <ng-template #content let-modal>
      <div class="modal-header">
        <div class="d-flex justify-content-end w-100 fs-3 text-white">
          <i
            class="mx-2 fa  fa fa-repeat"
            aria-hidden="true"
            title="右旋转"
            role="button"
            (click)="RotateRight()"
          ></i>
          <i
            class="mx-2 fa  fa-search-minus"
            aria-hidden="true"
            title="缩小"
            role="button"
            (click)="zoomOut()"
          ></i>
          <i
            class="mx-2 fa  fa-search-plus"
            aria-hidden="true"
            title="放大"
            role="button"
            (click)="zoomIn()"
          ></i>
          <i
            class="mx-2 fa  fa-times"
            aria-hidden="true"
            role="button"
            (click)="modal.dismiss('Cross click')"
          ></i>
        </div>
      </div>
      <div class="modal-body d-flex justify-content-center align-items-center ">
        <img
          width="400"
          class="modal-body-preview"
          [src]="_src"
          [style.transform]="'scale(' + zoom / 10 + ') rotate(' + rotate + 'deg)'"
        />
      </div>
    </ng-template>
  `,
  styles: [
    `
      .image-preview.imagehove > div {
        display: none;
      }

      .image-preview.imagehove:hover div {
        display: flex;
        align-items: center;
        justify-content: center;
      }

      ::ng-deep.dignite-preview .modal-content {
        background-color: transparent !important;
      }
      .object-fit-cover{
        object-fit: cover;
      }
    `,
  ],
  standalone: true,
  imports: [CoreModule, ThemeSharedModule, CommonModule, PageModule],
})
export class ImagePreviewComponent {
  /**图片链接 */
  _src: any = '';
  @Input()
  public set src(v: any) {
    this._src = v;
  }

  /**是否预览 */
  @Input() preview = true;

  /**宽 */
  @Input() width = 90;
  /**高 */
  @Input() height = 90;
  @Input() rounded = '';
  @Input() id = '';
  @Input() bgBlack = false;

  /**模态框实例 */
  modalRef!: NgbModalRef;

  /**放大倍数 */
  zoom: number = 10;

  /**旋转 */
  rotate: number = 0;
  constructor(
    private modalService:NgbModal
  ){

  }

  // private modalService = inject(NgbModal);

  @Output() deleteChange = new EventEmitter();
  @Output() checkChange = new EventEmitter();

  /**选择图片的事件，返回父组件是否选中 */
  checkImage(event: any) {
    this.checkChange.emit(event.target.checked);
  }

  /**删除图片 */
  deletecoverPics() {
    this.deleteChange.emit();
  }

  /**打开预览弹窗 */
  OpenPreviewImage(content: TemplateRef<any>) {
    this.modalRef = this.modalService.open(content, {
      fullscreen: true,
      modalDialogClass: 'dignite-preview',
    });
    this.modalRef.result.then(
      result => {},
      reason => {
        this.zoom = 10;
      }
    );
  }
  /**放大图像 */
  zoomIn() {
    let zoom = this.zoom;
    if (zoom == 20) return;
    zoom++;
    this.zoom = zoom;
  }

  /**缩小图像 */
  zoomOut() {
    let zoom = this.zoom;
    if (zoom == 3) return;
    zoom--;
    this.zoom = zoom;
  }

  /**右旋转 */
  RotateRight() {
    if (this.rotate == 360) return (this.rotate = 0);
    this.rotate += 90;
  }
}
