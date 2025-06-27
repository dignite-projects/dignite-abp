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
import { FormControl, FormGroup } from '@angular/forms';
import { addFieldControlGroup } from '../form';

@Component({
  selector: 'df-dynamic-config',
  templateUrl: './dynamic-config.component.html',
  styleUrl: './dynamic-config.component.scss',
})
export class DynamicConfigComponent implements AfterContentInit {
  constructor(@Inject('MERGED_FORM_CONFIG') private mergedConfig: any[]) {}

  formControlName: any = '';
  @Input() set type(v: string) {
    this.formControlName = v;
  }
  @Input() selected: any;

  formEntity: FormGroup | undefined;
  @Input() set form(v: FormGroup | undefined) {
    this.formEntity = v;
  }

  

  get formControlNameInput(){
    return this.formEntity.get('formControlName') as FormControl;
  }

  ngAfterContentInit(): void {
    this.loadfieldConfigComponent(this.formControlName);
    this.formControlNameInput?.valueChanges.subscribe((res: any) => {
      this.loadfieldConfigComponent(res);
    });
  }


  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormconfigRef', { read: ViewContainerRef, static: true })
  FormconfigRef?: ViewContainerRef;

  /**加载动态表单配置组件 */
  loadfieldConfigComponent(formControlName:string) {
    const _fieldControlGroup: any[] = addFieldControlGroup(this.mergedConfig);
    const fieldControlItem = _fieldControlGroup.find(el => el.name === formControlName);

    //清空了容器中的所有组件
    this.FormconfigRef?.clear();
    if (!fieldControlItem || !fieldControlItem.fieldConfigComponent) return;
    //在容器中创建组件
    const { instance }:any = this.FormconfigRef?.createComponent(fieldControlItem.fieldConfigComponent); //创建组件模板
    // /**向创建的组件模板中传值 */
    instance.selected = this.selected;
    instance.type = formControlName;
    instance.Entity = this.formEntity;
  }
}
