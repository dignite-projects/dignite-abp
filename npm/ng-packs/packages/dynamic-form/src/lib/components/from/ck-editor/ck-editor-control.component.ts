import { Component, ElementRef, Input, Renderer2, RendererFactory2, ViewChild, ViewContainerRef, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
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
import { isBase64UploadAdapter } from './ckEditorUpload';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ConfigStateService, CoreModule, RestService } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';


@Component({
  selector: 'df-ck-editor-control',
  templateUrl: './ck-editor-control.component.html',
  styleUrls: ['./ck-editor-control.component.scss'],
  standalone: true,
  imports: [
    CKEditorModule,
    FormsModule,
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
  ]
})
export class CkEditorControlComponent {

  languagesMap={
    ar:'ar',
    cs:'cs',
    en:'en',
    hi:'hi',
    fi:'fi',
    hu:'hu',
    fr:'fr',
    it:'it',
    'en-GB':'en-gb',
    'pt-BR':'pt-br',
    'zh-Hant':'zh',
    'zh-Hans':'zh-cn',
    tr:'tr',
    sk:'sk',
    'de-DE':'de',
    es:'es',
    ja:'ja',
    vi:'vi',
  }


  constructor(
    private fb: FormBuilder,
    private restService: RestService,
    private config: ConfigStateService
  ) {
    const currentCulture = this.config.getOne("localization")?.currentCulture?.name;
    this.editorConfig={
      language: this.languagesMap[currentCulture],
      placeholder: '',
      toolbar: {
        removeItems: ['mediaEmbed'],
        shouldNotGroupWhenFull: true
      },
      styles: [
        'alignCenter',
        'alignLeft',
        'alignRight'
      ],
      image: {
        toolbar: [
          'imageTextAlternative', 'toggleImageCaption', '|',
          'imageStyle:inline', 'imageStyle:wrapText', 'imageStyle:breakText', 'imageStyle:side', '|',
          , 'resizeImage'
        ],
        insert: {
          integrations: [
            'insertImageViaUrl'
          ]
        }
  
      }
    }
   }
  public Editor = ClassicEditor;

  /**富文本配置 */
  editorConfig = {}
  //**富文本加载完成 */
  public onReady(editor: any): void {
    let that = this
    editor.plugins.get('FileRepository').createUploadAdapter = function (loader: any) {
      return new isBase64UploadAdapter(loader, that.imagesContainerName,that.restService);
    };
  }
  /**富文本内容改变 */
  public onChange({ editor }: any) {
  }

















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
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit()
      this.submitclick.nativeElement.click();
    }
  }
  /**图片容器名称 */
  imagesContainerName: any
  AfterInit() {
    return new Promise((resolve, rejects) => {
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
