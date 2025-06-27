import { Injectable } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { FormControlStateDto } from './models';
import { AbstractControl } from '@angular/forms';

interface InvalidControlInfo {
  path: string;
  control: FormControl | FormArray;
  errors: any;
}

/**
 * 用于验证表单的服务
 * 包含表单验证方法和多语言翻译方法
 * @description 针对自定义表单暂未有更好的表单验证提示，继续使用本机的验证方式
 */
@Injectable({
  providedIn: 'root',
})
export class DigniteValidatorsService {
  constructor(private toaster: ToasterService, private _LocalizationService: LocalizationService) {}

  /**
   * 设置表单中指定多个字段禁用或者启用
   * @param input 包含表单、字段名称和禁用状态的输入对象
   * @returns 无返回值
   */
  setFormControlState(input: FormControlStateDto) {
    const { form, controlNames, disabled } = input;
    for (const name of controlNames) {
      const control = form.get(`${name}`) as FormControl;
      if (control && disabled) {
        control.disable();
      } else if (control && !disabled) {
        control.enable();
      }
    }
  }

  /**
   * 获取错误信息
   * @description 获取表单的错误信息，并根据映射对象进行本地化
   * @param form 表单
   * @param map 映射对象
   * @returns 错误信息
   */
  getErrorMessage(input: any): string|InvalidControlInfo[] {
    const { form, map,isShowToast=true } = input;
    const InvalidControls = this.findInvalidControls(form);
    // console.log(InvalidControls, 'InvalidControls');
    if(!isShowToast) return InvalidControls;
    const error: any = InvalidControls[0];

    // 获取本地化标签
    const getLocalizedLabel = (path: string) => {
      return this._LocalizationService.instant(map[path as keyof typeof map]);
    };

    // 格式化数组索引
    const formatArrayIndex = (item: string) => {
      const regex = /\[[0-9]+\]/g;
      const indexMatch = item.match(regex);
      const [basePath] = item.split(regex);
      return getLocalizedLabel(basePath) + (indexMatch ? indexMatch[0] : '');
    };

    let errorMessage = '';
    // 如果错误路径包含formConfiguration
    if (error.path.includes('formConfiguration')) {
      // 将错误路径中的formConfiguration-替换掉，并按-分割
      const pathParts = error.path.replaceAll('formConfiguration-', '').split('-');
      // 对分割后的路径进行格式化
      const formattedParts = pathParts.map((part, index) => {
        // 如果是最后一个路径且不是第一个路径
        if (index === pathParts.length - 1 && index > 0) {
          // 获取前一个路径
          const prevPart = pathParts[index - 1];
          // 将前一个路径按数组索引分割
          const [prevBasePath] = prevPart.split(/\[[0-9]+\]/);
          // 将当前路径按数组索引分割
          const [currentBasePath] = part.split(/\[[0-9]+\]/);
          // 组合前一个路径和当前路径
          const combinedKey = `${prevBasePath}-${currentBasePath}`;

          // 如果map中存在组合后的路径，则返回本地化标签，否则返回格式化后的数组索引
          return map[combinedKey as keyof typeof map]
            ? getLocalizedLabel(combinedKey)
            : formatArrayIndex(part);
        }
        // 否则返回格式化后的数组索引
        return formatArrayIndex(part);
      });
      // 将格式化后的路径用-连接起来
      errorMessage = formattedParts.join('-');
    }else if(error.path.includes('extraProperties')){
      const pathParts = error.path.replaceAll('extraProperties-', '');
      errorMessage=map[pathParts as keyof typeof map];
    } else {
      // 否则返回本地化标签
      errorMessage = getLocalizedLabel(error.path);
    }

    // 添加错误信息
    errorMessage = `${errorMessage} ${this._LocalizationService.instant(
      'AbpValidation::ThisFieldIsNotValid.',
    )}`;
    // 显示警告信息
    this.toaster.warn(errorMessage);
    return errorMessage;
  }

  findInvalidControls(control: AbstractControl, path = ''): InvalidControlInfo[] {
    const invalidControls: InvalidControlInfo[] = [];

    if (control instanceof FormControl) {
      if (control.invalid) {
        invalidControls.push({
          path,
          control,
          errors: control.errors,
        });
      }
    } else if (control instanceof FormGroup) {
      Object.keys(control.controls).forEach(key => {
        const childControl = control.controls[key];
        const childPath = path ? `${path}-${key}` : key;
        invalidControls.push(...this.findInvalidControls(childControl, childPath));
      });
    } else if (control instanceof FormArray) {
      if(control.invalid){
        invalidControls.push({
          path,
          control,
          errors: control.errors,
        });
      }
      control.controls.forEach((childControl, index) => {
        const childPath = `${path}[${index + 1}]`;
        invalidControls.push(...this.findInvalidControls(childControl, childPath));
      });
    }

    return invalidControls;
  }
}
