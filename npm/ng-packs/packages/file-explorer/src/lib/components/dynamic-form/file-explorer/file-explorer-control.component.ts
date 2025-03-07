import { ConfigStateService, EnvironmentService } from '@abp/ng.core';
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'fe-file-explorer-control',
  templateUrl: './file-explorer-control.component.html',
  styleUrls: ['./file-explorer-control.component.scss'],
})
export class FileExplorerControlComponent {
  constructor(private fb: FormBuilder) {}

  /**表单实体 */
  _entity: FormGroup | any;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    if (v) this.dataLoaded();
  }

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
    if (v) this.dataLoaded();
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
    if (v) this.dataLoaded();
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
    
    if (v && v.length > 0) this.dataLoaded();
  }

  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity.get('extraProperties') as FormGroup;
  }
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  /**字段配置 */
  formConfiguration: any = '';
  /**文件容器名称 */
  FileContainerName: any = '';
  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray:any = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      this.formConfiguration = this._fields.field.formConfiguration;
      this.FileContainerName = this.formConfiguration['FileExplorer.FileContainerName'];
      if (this._selected && this._selected.length > 0) {
        this._selected.map((el:any)=>{
          if(!el['url']){
            el['url']=this.GetImageUrl(el.blobName,el.containerName,el.tenantId)
          }
        })
        this.selectedFileGroup = this._selected;
      }
      let newControl = this.fb.control(this._selected, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);
      resolve(true);
    });
  }

  private configState = inject(ConfigStateService)
  private environment = inject(EnvironmentService)
  GetImageUrl(value: unknown, ...args: unknown[]): unknown {
    let tenantId = args[1]
    const environment = this.environment.getEnvironment();
    let imgUrl =`${environment.apis.default.url}/api/file-explorer/files/${args[0]}/${value}?__tenant=${tenantId}&timer=${new Date().getTime()}`
    return value?imgUrl:''
  }

  /**选择文件-弹窗的-已选定的文件 */
  selectedFileGroup: any[] = [];

  /**_selectedFile改变回调 */
  _selectedFileChange(event) {
    this.selectedFileGroup = event;
    let fieldName = this._fields.field.name;
    let obj = {};
    obj[fieldName] = event;
    this.extraProperties.patchValue(obj);
  }
}
