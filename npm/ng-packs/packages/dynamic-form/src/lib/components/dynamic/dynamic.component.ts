/* eslint-disable no-unsafe-optional-chaining */
/* eslint-disable @angular-eslint/component-selector */
import { Component, Inject, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FieldControlGroupInterfaces } from '../../interfaces';
import { AddFieldControlGroup, } from '../form';

@Component({
  selector: 'df-dynamic',
  templateUrl: './dynamic.component.html',
  styleUrls: ['./dynamic.component.scss'],
})
export class DynamicComponent {

  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v === undefined ? '' : v === null ? '' : v;
  }

  /**表单控件类型 */
  _type: string;
  @Input()
  public set type(v: string) {
    this._type = v;
    if (this._entity) this.dataLoaded(3);
  }

  /**语言 */
  _culture: FormGroup | undefined;
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
      this.dataLoaded(3);
    }
  }

  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormControlRef', { read: ViewContainerRef, static: true })
  FormControlRef?: ViewContainerRef;

  /**表单控件模板-动态表单组件 */
  @ViewChild('FormComponentsRef', { read: ViewContainerRef, static: true })
  FormComponentsRef?: ViewContainerRef;

  constructor(@Inject('MERGED_FORM_CONFIG') private mergedConfig: any[]) {}

  /**数据加载完成 */
  async dataLoaded(val) {
    const _fieldControlGroup: any[] = AddFieldControlGroup(this.mergedConfig);
    if (this._entity) {
      //加载所有的动态表单组件
      if (this._type) {
        const fieldControlItem = _fieldControlGroup.find(el => el.name === this._type);
        this.loadfieldConfigComponent(fieldControlItem);
      }
      if (this._fields && this._parentFiledName && this._culture) {
      
        /**表单控件组中的项 */
        const fieldControlItem = _fieldControlGroup.find(
          el => el.name === this._fields?.field?.formControlName
        );
        this.loadfieldComponent(fieldControlItem);
      }
    }
  }

  /**加载动态表单配置组件 */
  loadfieldConfigComponent(FieldControlItem?: FieldControlGroupInterfaces) {
   
    
    //清空了容器中的所有组件
    this.FormControlRef?.clear();
    if (!FieldControlItem || !FieldControlItem.fieldConfigComponent) return;
    //在容器中创建组件
    const { instance }:any = this.FormControlRef?.createComponent(
      FieldControlItem.fieldConfigComponent
    ); //创建组件模板
    /**向创建的组件模板中传值 */
    instance.selected = this._selected;
    instance.type = this._type;
    instance.Entity = this._entity;
  }

  /**加载动态表单组件 */
  loadfieldComponent(FieldControlItem?: FieldControlGroupInterfaces) {
    // this.FormControlRef.clear
    //清空了容器中的所有组件
    this.FormComponentsRef?.clear();
    if (!FieldControlItem || !FieldControlItem.fieldComponent) return;
    //在容器中创建组件
    const { instance }:any = this.FormComponentsRef?.createComponent(FieldControlItem.fieldComponent); //创建组件模板
    /**向创建的组件模板中传值 */
    instance.fields = this._fields;
    instance.parentFiledName = this._parentFiledName;
    instance.selected = this._selected;
    instance.culture = this._culture;
    instance.entity = this._entity;
    
  }
}
