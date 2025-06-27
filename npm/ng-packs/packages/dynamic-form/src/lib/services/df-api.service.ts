import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DfApiService {


  
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

 

}
