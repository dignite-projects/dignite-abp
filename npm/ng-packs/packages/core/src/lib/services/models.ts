import { FormGroup, FormArray } from "@angular/forms";

export interface validateAllFormDto {
    form: FormGroup | FormArray;
    //映射的字段名称，用于多语言翻译
    map: object;
    loopName?: string;
    //资源包名，用于多语言翻译
    resources:Array<string>;
  }
  
  export interface FormControlStateDto {
    form: FormGroup;
    controlNames: string[];
    disabled: boolean;
  }

  /**location-back.service.ts */
  export interface backBase {
    //检查的链接，如果链接中包含该字符串，则返回上一页，否则跳转到Url页
    url?: string;
    //链接补充，用于在url后面补充其余链接
    replenish?: string;
  }