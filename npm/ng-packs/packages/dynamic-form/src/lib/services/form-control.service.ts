import { Inject, Injectable } from '@angular/core';
import { FieldControlGroup } from '../components';

@Injectable({
  providedIn: 'root',
})
export class FormControlService {
  constructor(@Inject('MERGED_FORM_CONFIG') private mergedConfig: any[]) {}
  _FieldControlGroup = FieldControlGroup;

  AddFieldControlGroup() {
    let array=this.mergedConfig;
    for (const element of array) {
      let find = FieldControlGroup.find(control => {
        return control.name === element.name;
      });
      if (!find) {
        FieldControlGroup.push(element);
      }
    }
    return FieldControlGroup;
  }
}
