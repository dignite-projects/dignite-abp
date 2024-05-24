import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'df-select-control',
  templateUrl: './select-control.component.html',
  styleUrls: ['./select-control.component.scss']
})
export class SelectControlComponent {

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
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any
  @Input()
  public set selected(v: any) {
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
  formConfiguration: any = ''
  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray = []
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }
      this.formConfiguration = this._fields.field.formConfiguration
      if (!this._selected) {
        const isMultiple = this.formConfiguration['Select.Multiple'];

        let selectValue = isMultiple ? [] : ''
        this.formConfiguration['Select.Options'].forEach(el => {
          if (el.Selected) {
            selectValue = isMultiple ? [...selectValue, el.value||el.Value] : [el.value||el.Value];
            console.log(selectValue,'selectValueselectValue',this.formConfiguration['Select.Options']);
          }
        });
        this._selected = selectValue;
      }
      let newControl = this.fb.control(this._selected, ValidatorsArray)
      this.extraProperties.setControl(this._fields.field.name, newControl)
      resolve(true)
    })
  }


}
