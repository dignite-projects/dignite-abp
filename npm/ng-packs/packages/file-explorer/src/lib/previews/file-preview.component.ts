import { AfterContentInit, Component, Input, TemplateRef } from '@angular/core';
import { ImageTypeOption } from './models';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'fe-file-preview',
  templateUrl: './file-preview.component.html',
  styleUrls: ['./file-preview.component.scss']
})
export class FilePreviewComponent implements AfterContentInit{

  /**文件宽度 */
  @Input() width: any = '100px'
  /*文件链接 */
  @Input() src: any = ''
  /**是否支持大图预览 */
  @Input() preview: any = true;
  /**文件类型 */
  @Input() type: any = ''
  /**文件名称 */
  @Input() name: any = ''

  /**是否是文件 */
  isImage = true
  /**是否是视频 */
  isAudio = false
  /**是否是音频 */
  isVideo = false
  /**文件类型及图标 */
  _ImageTypeOption = ImageTypeOption


  constructor(
    private modalService: NgbModal
  ) { }
  
  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    if (!this.type) {
      this.type = this.name.includes('.7z') ? '7z' : ''
    }
    this.isImage = this.type.includes('image/')
    this.isAudio = this.type.includes('audio/')
    this.isVideo = this.type.includes('video/')
  }

  /**预览图片 */
  previewImage() {

  }

  /**模态框实例 */
  modalRef!: NgbModalRef;

  /**放大倍数 */
  zoom: number = 10
  /**旋转 */
  rotate: number = 0


  /**打开预览弹窗 */
  OpenPreviewImage(content: TemplateRef<any>) {

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
}
