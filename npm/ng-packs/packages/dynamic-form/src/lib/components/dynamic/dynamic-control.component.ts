/* eslint-disable no-unsafe-optional-chaining */
/* eslint-disable @angular-eslint/component-selector */
import {
  AfterContentInit,
  Component,
  Inject,
  Input,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
// import { FieldControlGroupInterfaces } from '@dignite-ng/expand.dynamic-form';
import { addFieldControlGroup } from '../form';

@Component({
  selector: 'df-dynamic-control',
  templateUrl: './dynamic-control.component.html',
  styleUrl: './dynamic-control.component.scss',
})
export class DynamicControlComponent implements AfterContentInit {
  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v === undefined ? '' : v === null ? '' : v;
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
      // this.dataLoaded(3);
    }
  }

  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormControlRef', { read: ViewContainerRef, static: true })
  FormControlRef?: ViewContainerRef;

  /**表单控件模板-动态表单组件 */
  @ViewChild('FormComponentsRef', { read: ViewContainerRef, static: true })
  FormComponentsRef?: ViewContainerRef;

  constructor(@Inject('MERGED_FORM_CONFIG') private mergedConfig: any[]) {}

  ngAfterContentInit(): void {
    this.loadfieldComponent(this._fields?.field?.formControlName);
  }

  // /**数据加载完成 */
  // async dataLoaded() {
  //   const _fieldControlGroup: any[] = addFieldControlGroup(this.mergedConfig);
  //   if (this._entity) {
  //     //加载所有的动态表单组件
  //     if (this._fields && this._parentFiledName && this._culture) {
  //       /**表单控件组中的项 */
  //       const fieldControlItem = _fieldControlGroup.find(
  //         el => el.name === this._fields?.field?.formControlName,
  //       );
  //       this.loadfieldComponent(fieldControlItem);
  //     }
  //   }
  // }

  /**加载动态表单组件 */
  loadfieldComponent(formControlName: string) {
    const _fieldControlGroup: any[] = addFieldControlGroup(this.mergedConfig);
    /**表单控件组中的项 */
    const fieldControlItem = _fieldControlGroup.find(el => el.name === formControlName);

    //清空了容器中的所有组件
    this.FormComponentsRef?.clear();
    if (!fieldControlItem || !fieldControlItem.fieldComponent) return;
    //在容器中创建组件
    const { instance }: any = this.FormComponentsRef?.createComponent(
      fieldControlItem.fieldComponent,
    ); //创建组件模板
    /**向创建的组件模板中传值 */
    instance.fields = this._fields;
    instance.parentFiledName = this._parentFiledName;
    instance.selected = this._selected;
    instance.culture = this._culture;
    instance.entity = this._entity;
  }
}
