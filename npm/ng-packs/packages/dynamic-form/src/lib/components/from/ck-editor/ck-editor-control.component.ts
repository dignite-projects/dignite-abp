import { Component, ElementRef, Inject, Input, Renderer2, ViewChild, inject, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isBase64UploadAdapter } from './ckEditorUpload';
import { RestService } from '@abp/ng.core';
import '@ckeditor/ckeditor5-build-classic/build/translations/zh-cn.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/zh.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/de.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/de-ch.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/ar';
import '@ckeditor/ckeditor5-build-classic/build/translations/cs.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/hi.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/fi.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/hu.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/fr.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/it.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/en-gb.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/pt-br.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/sk';
import '@ckeditor/ckeditor5-build-classic/build/translations/ja.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/es.js';
import '@ckeditor/ckeditor5-build-classic/build/translations/vi.js';
import { DOCUMENT } from '@angular/common';
import { ConfigStateService } from '@abp/ng.core';
declare var ClassicEditor: any;
@Component({
  selector: 'df-ck-editor-control',
  templateUrl: './ck-editor-control.component.html',
  styleUrls: ['./ck-editor-control.component.scss'],
})
export class CkEditorControlComponent {

 
  private _restService: RestService = inject(RestService)
  private config: ConfigStateService = inject(ConfigStateService)
  languagesMap = {
    ar: 'ar',
    cs: 'cs',
    en: 'en',
    hi: 'hi',
    fi: 'fi',
    hu: 'hu',
    fr: 'fr',
    it: 'it',
    'en-GB': 'en-gb',
    'pt-BR': 'pt-br',
    'zh-Hant': 'zh',
    'zh-Hans': 'zh-cn',
    tr: 'tr',
    sk: 'sk',
    'de-DE': 'de',
    es: 'es',
    ja: 'ja',
    vi: 'vi',
  }

  @ViewChild('ckeditor', { static: true }) ckeditor: ElementRef | undefined;
  public Editor: any;
  constructor(
    private renderer: Renderer2,
    @Inject(DOCUMENT) private document: Document
  ) {
    this.addScript('https://cdn.ckeditor.com/ckeditor5/41.4.1/classic/ckeditor.js')
  }

  addScript(url: string) {
    const script = this.renderer.createElement('script');
    this.renderer.setAttribute(script, 'type', 'text/javascript');
    this.renderer.setAttribute(script, 'src', url);
    script.onload = () => {
      this._init_Editor();
    }
    this.renderer.appendChild(this.document.head, script);
  }

  ClassicEditor_view:any
  // 初始化富文本
  _init_Editor() {
    const currentCulture = this.config.getOne("localization")?.currentCulture?.name;
  
    ClassicEditor.create(this.ckeditor?.nativeElement, {
      language: this.languagesMap[currentCulture],
      toolbar: {
        items: [
          'heading', '|', 'Alignment', 'FontSize', 'FontColor', 'FontFamily', 'Highlight', '|',
          'bold', 'italic',
          'link', '|',
          'bulletedList', 'numberedList',
          'insertTable', '|',
          'uploadImage', 'ImageResize', '|',
          'undo', 'redo'
        ],
        viewportTopOffset: 30,
        shouldNotGroupWhenFull: true
      },
      image: {
        toolbar: [
          'imageStyle:alignLeft', 'imageStyle:alignCenter', 'imageStyle:alignRight',
          '|',
          'resizeImage',
          '|',
          'imageTextAlternative'
        ],
        styles: [
          'alignLeft', 'alignCenter', 'alignRight'
        ],
        resizeOptions: [{
          name: 'resizeImage:original',
          label: 'Original',
          value: null
        },
        {
          name: 'resizeImage:25',
          label: '25%',
          value: '25'
        },
        {
          name: 'resizeImage:50',
          label: '50%',
          value: '50'
        },
        {
          name: 'resizeImage:75',
          label: '75%',
          value: '75'
        }
        ],
      },

    }).then((editor: any) => {
      this.Editor = editor;
      this.dataLoaded()
      var _this = this
      editor.plugins.get('FileRepository').createUploadAdapter = function (loader: any) {
        return new isBase64UploadAdapter(loader, _this.imagesContainerName, _this._restService);
      };
      editor.model.document.on('change:data', () => {
        const data = editor.getData();
        this.setckeditorInput(data)
      });
    }).catch((err: any) => {
      console.log(err)
    });
  }







  private fb = inject(FormBuilder)

  /**表单实体 */
  _entity: FormGroup | undefined
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded()
  }

  /**字段配置列表 */
  _fields: any = ''
  @Input()
  public set fields(v: any) {
    this._fields = v;
    this.dataLoaded()
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
    this.dataLoaded()
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v;
    this.dataLoaded()
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity.get('extraProperties') as FormGroup
  }
  get ckeditorInput() {
    return this.extraProperties.get(this._fields.field.name) as FormGroup
  }
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity && this.Editor) {
      await this.AfterInit()
      let fillingIn=this._selected||this._fields.field.formConfiguration['Ckeditor.InitialContent']
      this.Editor.setData(fillingIn);
      this.setckeditorInput(fillingIn)

      this.submitclick.nativeElement.click();
    }
  }

  invalidfeedback = false
  //设置值
  setckeditorInput(val) {
    this.invalidfeedback = true
    this.ckeditorInput.patchValue(val)
  }

  /**图片容器名称 */
  imagesContainerName: any = 111
  AfterInit() {
    return new Promise((resolve) => {
      let ValidatorsArray = []
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }

      let newControl = this.fb.control(this._selected ? this._selected : this._fields.field.formConfiguration['Ckeditor.InitialContent'], ValidatorsArray)
      this.extraProperties.setControl(this._fields.field.name, newControl)
      this.imagesContainerName = this._fields.field.formConfiguration['Ckeditor.ImagesContainerName']
      resolve(true)
    })
  }
}
