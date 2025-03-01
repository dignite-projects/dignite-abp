/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
/* eslint-disable @angular-eslint/use-lifecycle-interface */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TextEditMode } from '../../../enums/text-edit-mode';
import { TextEditConfig } from './text-edit-config';

@Component({
  selector: 'df-text-edit',
  templateUrl: './text-edit.component.html',
  styleUrls: ['./text-edit.component.scss']
})
export class TextEditComponent {

  constructor(
    private fb: FormBuilder,
  ) {
  }

  _TextEditMode = TextEditMode
  /**表单实体 */
  _entity: FormGroup | undefined
  @Input()
  public set entity(v: any) {
    this._entity = v;
    if (v) this.dataLoaded()
  }

  /**字段配置列表 */
  _fields: any = ''
  @Input()
  public set fields(v: any) {
    this._fields = v;
    if (v) this.dataLoaded()
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
    if (v) this.dataLoaded()
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v || '';
    if (v) this.dataLoaded()
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  extraProperties: FormGroup | undefined;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName) {
      this.extraProperties = this._entity.get(this._parentFiledName) as FormGroup
      await this.AfterInit()
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }


  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray:any[] = []
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }
      this._fields.field.formConfiguration={
        ...this.fb.group(new TextEditConfig()).value,
        ...this._fields.field.formConfiguration
      }
      if (this._fields.field.formConfiguration['TextEdit.CharLimit']) {
        ValidatorsArray.push(Validators.maxLength(this._fields.field.formConfiguration['TextEdit.CharLimit']))
      }
     
      let newControl = this.fb.control(this._selected, ValidatorsArray)
      this.extraProperties?.setControl(this._fields.field.name, newControl)
      resolve(true)
    })
  }
  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.extraProperties?.removeControl(this._fields.field.name)
  }

  isObjEmpty = (obj) => Object.keys(obj).length === 0
}
