import { Component, ContentChild, EventEmitter, Input, Output, TemplateRef, ViewChild, inject } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dignite-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {


  constructor(
    private modalService: NgbModal,
  ) { }



  @Input()
  get visible(): boolean {
    return this._visible;
  }
  set visible(value: boolean) {
    if (typeof value !== 'boolean') return;
    if (value) this.open(this.imageModalContent)
    if (!value) this.close(this.imageModalContent)
  }
@Input() size:string=''
@Input() fullscreen:boolean=false
@Input() class:string=''
  @Output() readonly visibleChange = new EventEmitter<boolean>();

  @ViewChild('imageModalContent') imageModalContent?: TemplateRef<any>;

  @ContentChild('digniteHeader', { static: false }) digniteHeader?: TemplateRef<any>;

  @ContentChild('digniteBody', { static: false }) digniteBody?: TemplateRef<any>;

  @ContentChild('digniteFooter', { static: false }) digniteFooter?: TemplateRef<any>;

  /**
   * 是否打开模态框
   */
  _visible: boolean = false;
  /**模态框实例 */
  modalRef!: NgbModalRef;

  /**
   * 打开模态框
   * @param content 
   */
  open(content?: TemplateRef<any>) {
    this.modalRef = this.modalService.open(content, {
      size: this.size,
      fullscreen: this.fullscreen,
      windowClass: `dignite-modal ${this.class}`,
    });
    this.modalRef.result.then(
      (result) => {
        this.visibleChange.emit(false);
      },
      (reason) => {
        this.visibleChange.emit(false);
      },
    );

  }
  /**
   * 关闭模态框
   * @param content 
   */
  close(content?: TemplateRef<any>) {
    this.modalRef?.dismiss()
  }




}
