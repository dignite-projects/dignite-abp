import { Injectable } from '@angular/core';
import { pinyin } from 'pinyin-pro';

@Injectable({
  providedIn: 'root',
})
export class ToPinyinService {
  get(value) {
    //去除字符串中所有的空格
    const val = value.replace(/[^a-zA-Z0-9\u4e00-\u9fa5-]/g, "");
    const array = val.split('');
    const newArray = [];
    array.forEach((el: any, index: any) => {
      //转化为小写
      let elChange = el.toLowerCase();
      const isChinese = str => {
        return /^[\u4e00-\u9fa5]+$/.test(str);
      };
      if (isChinese(elChange)) {
        const resultWithoutTone = pinyin(elChange, { toneType: 'none', type: 'array' });
        elChange = resultWithoutTone.toString();
        if (index < array.length - 1) elChange += '-';
      }
      newArray.push(elChange);
    });
    const pinyinstr = newArray.join('');
    return pinyinstr || val;
  }
}
