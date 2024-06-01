import { RestService } from '@abp/ng.core';
import { Component, ElementRef, EventEmitter, Inject, Input, Output, Renderer2, ViewChild, inject } from '@angular/core';
import { isBase64UploadAdapter } from '../../dynamic-form/ck-editor/ckEditorUpload';
import { LanguagesMap } from '../../enums/languages-map';
import('@dignite-ng/expand.ckeditor5-custom-build/build/translations/zh-cn.js')
import("@dignite-ng/expand.ckeditor5-custom-build/build/ckeditor.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/zh-cn.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/zh.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/de.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/de-ch.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/ar.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/cs.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/hi.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/fi.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/hu.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/fr.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/it.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/en-gb.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/pt-br.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/sk.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/ja.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/es.js");
import("@dignite-ng/expand.ckeditor5-custom-build/build/translations/vi.js");

declare var ClassicEditor: any
@Component({
  selector: 'ck-editor-base',
  templateUrl: './ck-editor-base.component.html',
  styleUrls: ['./ck-editor-base.component.scss']
})
export class CkEditorBaseComponent {
  private _restService: RestService = inject(RestService)
  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    this._init_Editor();
  }
  /** 语言目录，匹配系统语言，设置ckeditor的语言 */
  languagesMap=LanguagesMap
  
  /**选择html中的富文本容器 */
  @ViewChild('ckeditor', { static: true }) ckeditor: ElementRef | undefined;
  /**创建的ck-editor对象 */
  public Editor: any;
  /**图片容器名称 */
  _imageContainerName: any = ''
  @Input()
  public set imageContainerName(v: any) {
    if (!v) return
    this._imageContainerName = v;
    this.loadData()
  }

  /**ck-editor初始值 */
  _content: any = ''
  @Input()
  public set content(v: any) {
    if (!v) return
    this._content = v;
    this.loadData()
  }

  /**ck-editor配置 */
  _options: any = {}
  @Input()
  public set options(v: any) {
    if (!v) return
    this._options = v;
    this.loadData()
  }
 
  /**ck-editor值改变给父组件传值 */
  @Output() contentChange: EventEmitter<any> = new EventEmitter();
  /**ckeditor加载完成 */
  @Output() ready: EventEmitter<any> = new EventEmitter();

  /**加载完数据执行方法 */
  loadData() {
    if (this.Editor && this._content&&this._options) {
      this.Editor.setData(this._content);
      
    }
    
  }

  // 初始化富文本
  _init_Editor() {
    import('@dignite-ng/expand.ckeditor5-custom-build/build/ckeditor.js').then(ckeditorres=>{
      ckeditorres.default.create(this.ckeditor?.nativeElement, {
        ...this._options,
        language: this.languagesMap[this._options?.language||'en'],
        toolbar:this._options.toolbar|| {
          viewportTopOffset: 30,
          shouldNotGroupWhenFull: true
        },
      }).then((editor: any) => {
        this.Editor = editor;
        var _this = this
        this.ready.emit(editor)
        editor.plugins.get('FileRepository').createUploadAdapter = function (loader: any) {
          return new isBase64UploadAdapter(loader, _this._imageContainerName, _this._restService);
        };
        editor.model.document.on('change:data', () => {
          const data = editor.getData();
          this.contentChange.emit(data)
        });
        this.loadData()
      }).catch((err: any) => {
        console.log(err)
      });
    })
    
  }
}
