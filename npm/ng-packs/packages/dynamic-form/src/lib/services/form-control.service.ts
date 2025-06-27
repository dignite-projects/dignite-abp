import { Inject, Injectable } from '@angular/core';
import { FieldControlGroup } from '../components';

/**
 * 表单控件服务
 * @description 用于获取表单所有控件的配置信息
 */
@Injectable({
  providedIn: 'root',
})
export class FormControlService {
  constructor(@Inject('MERGED_FORM_CONFIG') private mergedConfig: any[]) {}
  _FieldControlGroup = FieldControlGroup;

  addFieldControlGroup() {
    const array=this.mergedConfig;
    for (const element of array) {
      const find = FieldControlGroup.find(control => {
        return control.name === element.name;
      });
      if (!find) {
        FieldControlGroup.push(element);
      }
    }
    return FieldControlGroup;
  }
}
