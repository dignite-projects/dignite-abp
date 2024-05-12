import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileApiService {

  constructor() { }

  /**转换文件大小单位 输入k */
  formatFileSize(bytes: any) {
    const units = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    let size = bytes;
    let unitIndex = 0;

    while (size >= 1024 && unitIndex < units.length - 1) {
      size /= 1024;
      unitIndex++;
    }
    // 保留两位小数，四舍五入
    size = size.toFixed(1)
    return `${size} ${units[unitIndex]}`;
  }
  /**获取图片的本地连接 */
  getImageLacolBase64Url(file: File) {
    return new Promise((resolve, rejects) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        resolve(e.target.result)
      };
      reader.onerror = error => rejects(error);
    })
  }
  /**
    * 深拷贝--方法
    * $api.deepClone()  */
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
