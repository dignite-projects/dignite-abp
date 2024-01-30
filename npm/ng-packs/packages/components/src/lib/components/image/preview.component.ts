import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dignite-image',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.scss']
})
export class PreviewComponent {
  constructor(
    private modalService: NgbModal
  ) { }

  /**固定显示图像的类 */
  @Input() class: any



  /**规定显示图像的 URL。 */
  @Input() src: string

  /**规定图像的宽度。 */
  @Input() width: string|number=120

  /**规定图像的高度。 */
  @Input() height: string|number=120

  /**规定图像的替代文本 */
  @Input() alt: string=''

  /**规定图像鼠标移入时显示的文本 */
  @Input() title: string=''

  /**是否支持删除 */
  @Input() isdelete: boolean=false
  /**是否允许预览 */
  @Input() ispreview: boolean=true


  @Output() deleteClick = new EventEmitter<boolean>();

  /**模态框实例 */
  modalRef!: NgbModalRef;

  /**放大倍数 */
  zoom: number = 10
  /**旋转 */
  rotate: number = 0



  /**打开预览弹窗 */
  openFullscreen(content: TemplateRef<any>) {

    this.modalRef = this.modalService.open(content, {
      fullscreen: true,
      modalDialogClass: 'dignite-preview',
    });
    this.modalRef.result.then(
      (result) => {

      },
      (reason) => {
        this.zoom = 10
      },
    );
  }
  /**放大图像 */
  zoomIn() {
    let zoom = this.zoom
    if (zoom == 20) return
    zoom++
    this.zoom = zoom
  }
  /**缩小图像 */
  zoomOut() {
    let zoom = this.zoom
    if (zoom == 3) return
    zoom--
    this.zoom = zoom
  }
  /**右旋转 */
  RotateRight() {
    if (this.rotate == 360) return this.rotate = 0
    this.rotate += 90
  }
  /**删除回调 */
  deteleImage() {
    this.deleteClick.emit()
  }
}
