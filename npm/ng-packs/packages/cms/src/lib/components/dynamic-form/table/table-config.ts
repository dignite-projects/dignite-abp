import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";

export class TableConfig {



    // 选项
    TableColumns: any = new FormArray([])

    constructor(data?: TableConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}
export class TableFormControl {
    /**列标题 */
    displayName: any = ['', [Validators.required]];
    /**空间配置 */
    formConfiguration: any = new FormGroup({})
    /**列名 */
    name: any = ['', [Validators.required]];
    //控件标识
    formControlName: any = ['', [Validators.required]];

    /**是否必填 */
    required: any = [false];
    /**描述 */
    description: any = [''];


    constructor(data?: TableConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}