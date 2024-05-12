import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";

export class MatrixConfig {
    // 矩阵类型
    'MatrixBlockTypes': any = new FormArray([])

    constructor(data?: MatrixConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}
export class MatrixItemConfig {
  
    displayName: any = ['', [Validators.required]];
    /**字段名字 */
    name: any = ['', [Validators.required]];

    constructor(data?: MatrixConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }


    }
}



export class matrixFieldInputBase {


    /**字段名称 Display name of this field */
    displayName: any = ['', [Validators.required]];
    /**字段唯一名称 Unique Name*/
    name: any = ['', [Validators.required]];
    /**描述 说明 */
    description: any = ['', []];
    /**FieldType字段类型 表单控件名称 */
    formControlName: any = ['TextEdit', [Validators.required]];
   /**动态表单配置 */
    formConfiguration: FormGroup | undefined = new FormGroup({})


    constructor(data?: matrixFieldInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }

    }
}