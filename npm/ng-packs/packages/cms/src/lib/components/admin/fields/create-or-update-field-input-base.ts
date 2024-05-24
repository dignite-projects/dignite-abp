import { FormGroup, Validators } from "@angular/forms";

export class CreateOrUpdateFieldInputBase {
    /**分组id */
    groupId: any = ['', []];

    /**字段名称 Display name of this field */
    displayName: any = ['', [Validators.required]];

    /**字段唯一名称 Unique Name*/
    name: any = ['', [Validators.required]];

    /**描述 说明 */
    description: any = ['', []];

    /**FieldType字段类型 表单控件名称 */
    formControlName: any = [undefined, [Validators.required]];

    /**动态表单配置 */
    formConfiguration: FormGroup | undefined = new FormGroup({})


    constructor(data?: CreateOrUpdateFieldInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }

    }
}
