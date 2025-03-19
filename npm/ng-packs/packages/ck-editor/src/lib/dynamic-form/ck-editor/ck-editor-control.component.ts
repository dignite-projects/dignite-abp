/* eslint-disable no-async-promise-executor */
/* eslint-disable @typescript-eslint/no-this-alias */
/* eslint-disable @angular-eslint/component-selector */
import {
  ConfigStateService,
  LazyLoadService,
  LOADING_STRATEGY,
  RestService,
  SubscriptionService,
} from '@abp/ng.core';
import { ChangeDetectorRef, Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isBase64UploadAdapter } from './ckEditorUpload';
import { SetCkEditorConfigsService } from '../../services';
import { CkEditorModeEnum } from '../../enums/ck-editor-mode.enum';

@Component({
  selector: 'ck-editor-control',
  templateUrl: './ck-editor-control.component.html',
  styleUrls: ['./ck-editor-control.component.scss'],
})
export class CkEditorControlComponent {

  private config: ConfigStateService = inject(ConfigStateService);
  private _restService: RestService = inject(RestService);
  /**ck-Editor的值 */
  ckEditorValue: any = '';
  /**系统语言 */
  currentCulture = this.config.getOne('localization')?.currentCulture?.name;

  CkEditorModeEnum = CkEditorModeEnum;

  constructor() {}
  public onReady(editor) {
    const _this = this;
    editor.plugins.get('FileRepository').createUploadAdapter = function (loader: any) {
      return new isBase64UploadAdapter(loader, _this.imagesContainerName, _this._restService);
    };
  }

  /**ck-editor配置 */
  ckOptions: any;
  /**富文本内容改变 */
  ckEditorChange(event) {
    this.setckeditorInput(event);
  }

  private fb = inject(FormBuilder);

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }
  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity?.get('extraProperties') as FormGroup;
  }
  get ckeditorInput() {
    return this.extraProperties.get(this._fields.field.name) as FormGroup;
  }
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();
      this.ckEditorValue =
        this._selected || this._fields.field.formConfiguration['Ckeditor.InitialContent'];
      this.setckeditorInput(this.ckEditorValue);
      // this.cdr.detectChanges(); // 手动触发变更检测
        await this.loadckeditor();
      // this.submitclick?.nativeElement?.click();
    }
  }
  private _SetCkEditorConfigsService = inject(SetCkEditorConfigsService);
  public Editor: any;
  loadckeditor() {
    const _that = this;
    const formConfiguration=this._fields.field.formConfiguration;
    return new Promise(async resolve => {
      await import('ckeditor5').then(async res => {
        this.loadStyle();
        console.log('ckeditor5', res);
        if(formConfiguration['Ckeditor.Mode']==CkEditorModeEnum.Simple){
          _that.Editor = res.InlineEditor;
        } else if(formConfiguration['Ckeditor.Mode']==CkEditorModeEnum.Classic){
          _that.Editor = res.ClassicEditor;
        }else{
          formConfiguration['Ckeditor.Mode']=CkEditorModeEnum.Simple;
          _that.Editor = res.InlineEditor;
        }
        const configs:any = await _that._SetCkEditorConfigsService.get({
          language: _that.currentCulture,
          type:formConfiguration['Ckeditor.Mode']
        });
        if (!this.imagesContainerName) {
          if(configs?.toolbar?.items?.indexOf('insertImage')!=-1){
            configs?.toolbar?.items?.splice(
              configs?.toolbar?.items?.indexOf('insertImage'),
              1
            );
          }
        }
        _that.ckOptions = configs;
      });
      resolve(true);
    });
  }
  private lazyLoadService = inject(LazyLoadService);
  private subscriptionService = inject(SubscriptionService);
  private loadStyle() {
    const loaded$ = this.lazyLoadService.load(
      LOADING_STRATEGY.AppendAnonymousStyleToHead('ckeditor5.css')
    );
    this.subscriptionService.addOne(loaded$);
  }

  invalidfeedback = false;
  //设置值
  setckeditorInput(val) {
    this.invalidfeedback = true;
    this.ckeditorInput.patchValue(val);
  }

  /**图片容器名称 */
  imagesContainerName: any = '';
  AfterInit() {
    return new Promise(resolve => {
      const ValidatorsArray:any[] = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      const newControl = this.fb.control(
        this._selected
          ? this._selected
          : this._fields.field.formConfiguration['Ckeditor.InitialContent'],
        ValidatorsArray
      );
      this.extraProperties.setControl(this._fields.field.name, newControl);
      this.imagesContainerName =
        this._fields.field.formConfiguration['Ckeditor.ImagesContainerName'];

    
      resolve(true);
    });
  }
}
