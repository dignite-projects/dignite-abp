import { Injectable } from '@angular/core';
import { pinyin } from 'pinyin-pro';

@Injectable({
  providedIn: 'root',
})
export class CmsApiService {
  constructor() {}

  /**
   * 识别中文转化为拼音，固定返回类型
   * 汉字转拼音
   * 大写转小写
   *
   *  */
  chineseToPinyin(value: any) {
    //去除字符串中所有的空格
    let val = value.replaceAll(' ', '-');
    let array = val.split('');
    let newArray = [];
    array.forEach((el: any, index: any) => {
      //转化为小写
      let elChange = el.toLowerCase();
      let isChinese = str => {
        return /^[\u4e00-\u9fa5]+$/.test(str);
      };
      if (isChinese(elChange)) {
        const resultWithoutTone = pinyin(elChange, { toneType: 'none', type: 'array' });
        elChange = resultWithoutTone.toString();
        if (index < array.length - 1) elChange += '-';
      }
      newArray.push(elChange);
    });
    let pinyinstr = newArray.join('');
    return pinyinstr || val;
  }

  /**
   * 深拷贝--方法  */
  deepClone(obj) {
    if (typeof obj !== 'object' || obj === null) return obj;
    const result = Array.isArray(obj) ? [] : {};
    for (let key in obj) {
      if (obj.hasOwnProperty(key)) {
        if (typeof obj[key] === 'object' && obj[key] !== null) {
          if (obj[key] instanceof Date) {
            result[key] = new Date(obj[key].getTime());
          } else if (obj[key] instanceof RegExp) {
            result[key] = new RegExp(obj[key]);
          } else {
            result[key] = this.deepClone(obj[key]);
          }
        } else {
          result[key] = obj[key];
        }
      }
    }
    return result;
  }
// this._selected = this.convertKeysToCamelCase(this._selected);
    /**
     * 递归将对象属性名首字母转为小写
     * @param obj 需要处理的对象或数组
     * @param isInsideFormConfig 标记是否在 FormConfiguration 内部
     * @returns 处理后的新对象
     */
    convertKeysToCamelCase(obj: any, isInsideFormConfig = false): any {
      
      if (Array.isArray(obj)) {
        return obj.map(item => this.convertKeysToCamelCase(item, isInsideFormConfig));
      } else if (typeof obj === 'object' && obj !== null) {
        return Object.keys(obj).reduce((acc, key) => {
          
          // 判断当前层级是否在 FormConfiguration 内部
          const currentIsInsideFormConfig =  isInsideFormConfig|| key === 'MatrixBlockTypes';
          
          // 如果在 FormConfiguration 内部，保留原字段名
          const newKey = currentIsInsideFormConfig ? key : key.charAt(0).toLowerCase() + key.slice(1);
          // 递归处理子属性，并传递是否在 FormConfiguration 内部的状态
          acc[newKey] = this.convertKeysToCamelCase(obj[key], key === 'FormConfiguration');
          return acc;
        }, {} as Record<string, any>);
      }
      return obj;
    }




}
