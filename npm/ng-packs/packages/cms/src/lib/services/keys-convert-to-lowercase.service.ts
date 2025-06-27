import { Injectable } from '@angular/core';

/**
 * Converts all keys of an object to lowercase.
 * 将对象所有建转化为小写
 */
@Injectable({
  providedIn: 'root'
})
export class KeysConvertToLowercaseService {

   /**
     * 递归将对象属性名首字母转为小写
     * @param obj 需要处理的对象或数组
     * @param isInsideFormConfig 标记是否在 FormConfiguration 内部
     * @returns 处理后的新对象
     */
    get(obj: any, isInsideFormConfig = false): any {
      
      if (Array.isArray(obj)) {
        return obj.map(item => this.get(item, isInsideFormConfig));
      } else if (typeof obj === 'object' && obj !== null) {
        return Object.keys(obj).reduce((acc, key) => {
          // 判断当前层级是否在 FormConfiguration 内部
          const currentIsInsideFormConfig =  isInsideFormConfig|| key === 'MatrixBlockTypes'||key==='TableColumns'||key.includes('.');
          // 如果在 FormConfiguration 内部，保留原字段名
          const newKey = currentIsInsideFormConfig ? key : key.charAt(0).toLowerCase() + key.slice(1);
          // 递归处理子属性，并传递是否在 FormConfiguration 内部的状态
          acc[newKey] = this.get(obj[key], key === 'FormConfiguration');
          return acc;
        }, {} as Record<string, any>);
      }
      return obj;
    }
}
