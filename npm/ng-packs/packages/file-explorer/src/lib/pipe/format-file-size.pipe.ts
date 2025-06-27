import { Pipe, PipeTransform } from '@angular/core';


/**
 * 转换文件大小单位 输入k 
 * @description 用于转换文件大小单位 输入k
 * @returns {string} 文件大小单位 输入k
 */
@Pipe({
  name: 'formatFileSize',
  standalone: true
})
export class FormatFileSizePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return this.get(value);
  }

  
  /**转换文件大小单位 输入k */
  get(bytes: any) {
    const units = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    let size:any = bytes;
    let unitIndex = 0;
    while (size >= 1024 && unitIndex < units.length - 1) {
      size /= 1024;
      unitIndex++;
    }
    // 保留两位小数，四舍五入
    size = size.toFixed(1)
    return `${size} ${units[unitIndex]}`;
  }

}
