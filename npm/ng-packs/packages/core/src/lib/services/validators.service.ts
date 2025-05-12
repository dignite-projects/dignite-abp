import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Injectable } from '@angular/core';
import { FormGroup, FormArray, FormControl, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class ValidatorsService {
  constructor(private toaster: ToasterService, private _LocalizationService: LocalizationService) {}

  /**检查表单- */
  isCheckForm(input: { [key: string]: boolean }, module = 'AbpValidation'): boolean {
    const keys = Object.keys(input);
    for (let i = 0; i < keys.length; i++) {
      const element = keys[i];
      if (input[element] === false) {
        const displayName = element.charAt(0).toUpperCase() + element.slice(1);
        console.log(element,'element',displayName);
        let info = `"${this._LocalizationService.instant(`${module}::${displayName}`)}" `;
        if (element.includes('.') && !element.includes('].')) {
          const arr = element.split('.');
          info = `"${this._LocalizationService.instant(
            `${module}::${arr[0]}`,
          )}.${this._LocalizationService.instant(`${module}::${arr[1]}`)}"`;
        }
        if (element.includes('].')) {
          const arr = element.split('].');
          const arrStart = arr[0].split('[');
          info = `"${this._LocalizationService.instant(`${module}::${arrStart[0]}`)}[${
            arrStart[1]
          }].${this._LocalizationService.instant(`${module}::${arr[1]}`)}"`;
        }
        info = info + this._LocalizationService.instant(`AbpValidation::ThisFieldIsNotValid.`);

        //使用abp多语言提示
        this.toaster.warn(info);
        return true;
      }
    }
    return false;
  }

  /**获取表单所有字段是否通过验证 */
  getFormValidationStatus(formEntity: FormGroup | FormArray): { [key: string]: any } {
    const validationStatus: { [key: string]: any } = {};
    
    // 递归遍历表单组和表单控件集合
    const traverseForm = (form: FormGroup | FormArray, prefix = '') => {
      if (form instanceof FormGroup) {
        Object.keys(form.controls).forEach(key => {
          const control = form.controls[key];
          // const displayName = key.charAt(0).toUpperCase() + key.slice(1);
          const displayName = key;
          const fullKey = prefix ? `${prefix}.${displayName}` : displayName;
          if (control instanceof FormControl) {
            validationStatus[fullKey] = control.valid;
          } else if (control instanceof FormArray) {
            traverseForm(control, fullKey);
          } else if (control instanceof FormGroup) {
            traverseForm(control, fullKey);
          }
        });
      } else if (form instanceof FormArray) {
        form.controls.forEach((control, index) => {
          const fullKey = prefix ? `${prefix}[${index}]` : `[${index}]`;
          if (control instanceof FormControl) {
            validationStatus[fullKey] = control.valid;
          } else if (control instanceof FormArray) {
            traverseForm(control, fullKey);
          } else if (control instanceof FormGroup) {
            traverseForm(control, fullKey);
          }
        });
      }
    };

    traverseForm(formEntity);

    return validationStatus;
  }
}
