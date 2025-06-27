import { Injectable } from '@angular/core';

/**
 * 获取图片的本地连接
 * @description 用于获取图片的本地连接
 *  */
@Injectable({
  providedIn: 'root',
})
export class GetImageLacolBase64urlService {
  
  get(file: File) {
    return new Promise((resolve, rejects) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        resolve(e.target.result);
      };
      reader.onerror = error => rejects(error);
    });
  }
}
