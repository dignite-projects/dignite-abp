/* eslint-disable @angular-eslint/component-selector */
import { Component, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { FormControlService } from '../../services/form-control.service';
import { FieldControlGroupInterfaces } from '../../interfaces';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'df-dynamic-search',
  templateUrl: './dynamic-search.component.html',
  styleUrl: './dynamic-search.component.scss',
})
export class DynamicSearchComponent {
  constructor(private _FormControlService: FormControlService) {
  }

 

  /**语言 */
  _culture: FormGroup | any;
  @Input()
  public set culture(v: any) {
    this._culture = v;
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
  }
  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    if (v) {
      this._entity = v;
    }
  }


  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormSearchTemplateRef', { read: ViewContainerRef, static: true })
  FormSearchTemplateRef?: ViewContainerRef;

  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    if (this._fields && this._entity&&this._culture) {
      let _fieldControlGroup: any[] = this._FormControlService.addFieldControlGroup();
      let fieldControlItem = _fieldControlGroup.find(el => el.name === this._fields?.field?.formControlName);
      this.loadComponent(fieldControlItem);
    }
  }

  /**加载动态展示组件 */
  loadComponent(FieldControlItem?: FieldControlGroupInterfaces) {
    //清空了容器中的所有组件
    this.FormSearchTemplateRef?.clear();
    if (!FieldControlItem || !FieldControlItem.fieldSearchComponent) return;
    //在容器中创建组件
    const { instance }: any = this.FormSearchTemplateRef?.createComponent(
      FieldControlItem.fieldSearchComponent,
    ); //创建组件模板
    
    /**向创建的组件模板中传值 */
    instance.fields = this._fields;
    instance.parentFiledName = this._parentFiledName;
    instance.culture = this._culture;
    instance.entity = this._entity;
  }
}
