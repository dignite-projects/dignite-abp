import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'df-switch-control',
  templateUrl: './switch-control.component.html',
  styleUrls: ['./switch-control.component.scss']
})
export class SwitchControlComponent {

  constructor(
    private fb: FormBuilder,
  ) {

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
  /** */
  _selected: any
  @Input()
  public set selected(v: any) {
    // ?v:false;
    this._selected = v
    
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

  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray = []
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }
      let newControl = this.fb.control(this._selected?this._selected:this._selected===false?this._selected:this._fields.field.formConfiguration['Switch.Default'], ValidatorsArray)
      this.extraProperties.setControl(this._fields.field.name, newControl)
      resolve(true)
    })
  }
}
