import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { pinyin } from 'pinyin-pro';

@Injectable({
  providedIn: 'root'
})
export class CmsApiService {

  constructor(
    private titleService: Title,
  ) { }
  /**动态设置浏览器标题 */
  setBrowserTitle(title:string){
    this.titleService.setTitle(title)
  }
  

  /**
   * 识别中文转化为拼音，固定返回类型
   * 汉字转拼音
   * 大写转小写
   * 
   *  */
  chineseToPinyin(value: any) {
    //去除字符串中所有的空格
    // let val = value
    let val = value.replaceAll(' ', "-")
    let array = val.split('')
    let newArray = []
    array.forEach((el: any, index: any) => {
      //转化为小写
      let elChange = el.toLowerCase()
      let isChinese = (str) => {
        return /^[\u4e00-\u9fa5]+$/.test(str);
      }
      if (isChinese(elChange)) {
        const resultWithoutTone = pinyin(elChange, { toneType: 'none', type: 'array' });
        elChange = resultWithoutTone.toString() 
        if (index < array.length - 1)  elChange+='-'
      };
      newArray.push(elChange)
    });
    let pinyinstr = newArray.join('')
    return pinyinstr || val
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
}
