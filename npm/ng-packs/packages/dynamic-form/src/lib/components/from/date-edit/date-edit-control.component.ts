import { Component, ElementRef, Input, ViewChild,ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DateEditInterfaces } from '../../../enums/date-edit-interfaces';
  
@Component({
  selector: 'df-date-edit-control',
  templateUrl: './date-edit-control.component.html',
  styleUrls: ['./date-edit-control.component.scss']
})
export class DateEditControlComponent {

  constructor(
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef 
  ) {

  }
  _DateEditInterfaces=DateEditInterfaces
  /**表单实体 */
  _entity: FormGroup | undefined
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded()
  }

  public get entity(): any {
    return this._entity
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
    this._selected = v ;
    this.dataLoaded()
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;


  get extraProperties() {
    return this._entity.get('extraProperties') as FormGroup
  }
  get fieldInput() { return this.extraProperties.get(this._fields.field.name); }
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit()
      this.cdr.detectChanges();  
      this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray = []
      let formConfiguration=this._fields.field.formConfiguration
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }
      if (formConfiguration['DateEdit.Min']) {
        ValidatorsArray.push(Validators.min(formConfiguration['DateEdit.Min']))
      }
      if (formConfiguration['DateEdit.Max']) {
        ValidatorsArray.push(Validators.max(formConfiguration['DateEdit.Max']))
      }
      
      let newControl = this.fb.control(this._selected, ValidatorsArray)
      this.extraProperties.setControl(this._fields.field.name, newControl)

      resolve(true)
    })
  }
}