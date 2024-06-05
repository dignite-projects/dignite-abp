import { ConfigStateService, RestService } from '@abp/ng.core';
import { Component, ElementRef,  Input, ViewChild, inject, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import "ckeditor5-custom-build/build/translations/zh-cn"
import "ckeditor5-custom-build/build/translations/de-ch";
import "ckeditor5-custom-build/build/translations/en-gb";
import "ckeditor5-custom-build/build/translations/pt-br";
import "ckeditor5-custom-build/build/translations/zh";
import "ckeditor5-custom-build/build/translations/de";
import "ckeditor5-custom-build/build/translations/ar";
import "ckeditor5-custom-build/build/translations/cs";
import "ckeditor5-custom-build/build/translations/hi";
import "ckeditor5-custom-build/build/translations/fi";
import "ckeditor5-custom-build/build/translations/hu";
import "ckeditor5-custom-build/build/translations/fr";
import "ckeditor5-custom-build/build/translations/it";
import "ckeditor5-custom-build/build/translations/sk";
import "ckeditor5-custom-build/build/translations/ja";
import "ckeditor5-custom-build/build/translations/es";
import "ckeditor5-custom-build/build/translations/vi";
import { LanguagesMap } from '../../enums/languages-map';
import { isBase64UploadAdapter } from './ckEditorUpload';

@Component({
  selector: 'ck-editor-control',
  templateUrl: './ck-editor-control.component.html',
  styleUrls: ['./ck-editor-control.component.scss'],
})
export class CkEditorControlComponent {
  private config: ConfigStateService = inject(ConfigStateService)
  private _restService: RestService = inject(RestService)
  /**ck-Editor的值 */
  ckEditorValue: any = '<p>aubznahsj</p>'
  /**系统语言 */
  currentCulture = this.config.getOne("localization")?.currentCulture?.name;

    /** 语言目录，匹配系统语言，设置ckeditor的语言 */
    languagesMap=LanguagesMap

  public Editor: any;
  constructor() {
    import('ckeditor5/build/ckeditor').then(res => {
      this.Editor = res.default
      this.ckOptions = {
        language: this.languagesMap[this.currentCulture],
        toolbar: {
          items: [
            'heading', '|',
            'bold', 'italic',
            'link', '|',
            'bulletedList', 'numberedList',
            'insertTable', '|',
            'uploadImage', '|',
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
      }
    })
  }
  public onReady(editor){
    let _this=this
    editor.plugins.get('FileRepository').createUploadAdapter = function (loader: any) {
      return new isBase64UploadAdapter(loader, _this.imagesContainerName, _this._restService);
    };
  }

  /**ck-editor配置 */
  ckOptions: any 
  /**富文本内容改变 */
  ckEditorChange(event) {
    this.setckeditorInput(event)
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
    if (this._fields && this._entity ) {
      await this.AfterInit()
      // let fillingIn=this._selected||this._fields.field.formConfiguration['Ckeditor.InitialContent']
      // this.Editor.setData(fillingIn);
      this.ckEditorValue=this._selected||this._fields.field.formConfiguration['Ckeditor.InitialContent']
      this.setckeditorInput(this.ckEditorValue)
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
  imagesContainerName: any = ''
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
