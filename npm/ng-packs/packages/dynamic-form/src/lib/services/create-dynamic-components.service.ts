import { Injectable, Input } from '@angular/core';
import { FieldControlGroupInterfaces } from '../interfaces';
import { FieldControlGroup } from '../components';

@Injectable({
  providedIn: 'root'
})
export class CreateDynamicComponentsService {

  constructor() { }

  /**表单控件组 */
  _fieldControlGroup: FieldControlGroupInterfaces[] = FieldControlGroup

  /**加载动态表单配置组件 */
  loadfieldConfigComponent(input: any) {
   
   
    
    let { configRef, type, entity, selected } = input
    let fieldControlItem = this._fieldControlGroup.find(el => el.name === type)
    //清空了容器中的所有组件
    configRef?.clear();
    if (!fieldControlItem || !fieldControlItem.fieldConfigComponent) return
    //在容器中创建组件
    const { instance } = configRef?.createComponent(fieldControlItem.fieldConfigComponent);//创建组件模板
    console.log('加载动态表单配置组件',input,instance);
    /**向创建的组件模板中传值 */
    instance.Entity = entity
    instance.selected = selected
    instance.type = type
  }

   /**加载动态表单组件 */
   loadfieldComponent(input: any){
    let { controlRef, entity,fields,parentFiledName, selected,culture } = input
    let fieldControlItem = this._fieldControlGroup.find(el => el.name === fields.field.formControlName)
        //清空了容器中的所有组件
    controlRef?.clear();
    if (!fieldControlItem || !fieldControlItem.fieldComponent) return
    //在容器中创建组件
    const { instance } = controlRef?.createComponent(fieldControlItem.fieldComponent);//创建组件模板
    /**向创建的组件模板中传值 */
    instance.entity = entity
    instance.fields = fields
    instance.parentFiledName = parentFiledName
    instance.selected = selected
    instance.culture = culture
   }

}
