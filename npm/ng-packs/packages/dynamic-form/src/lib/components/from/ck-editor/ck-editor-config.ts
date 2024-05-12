import { FormControl, Validators } from "@angular/forms";

export class CkEditorConfig {


    /**文件容器名称 */
    // placeholder: any = new FormControl('', Validators.required);
    'Ckeditor.ImagesContainerName': any = ['', []];
      /**多选 */
    // placeholder: any = new FormControl('', Validators.required);
    'Ckeditor.InitialContent': any = ['', []];
  

    constructor(data?: CkEditorConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }


    }
}