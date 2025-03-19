import { FormControl, Validators } from "@angular/forms";
import { CkEditorModeEnum } from "../../enums/ck-editor-mode.enum";

export class CkEditorConfig {

    /**类型 */
    'Ckeditor.Mode': any = [CkEditorModeEnum.Simple, []];
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