import { FormControl, Validators } from "@angular/forms";

export class TextEditConfig {


   /**占位符 */
    'TextEdit.Placeholder': any = ['', []];
    //字段类型，单行文本，多行文本
    'TextEdit.Mode': any = [0, []];
    // 字数限制
    'TextEdit.CharLimit': any = ['265', []];

    constructor(data?: TextEditConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }


    }
}