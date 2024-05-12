import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class CreateOrUpdateSitesInputBase {

    /**字段名称 Display name of this field */
    displayName: any = ['', [Validators.required]];

    /**字段唯一名称 Unique Name*/
    name: any = ['', [Validators.required]];

    /**主机地址 */
    host: any = ['https://', [Validators.required]];

    /**是否激活 */
    isActive: any = [true, []];
    // 语言
    languages:any = [[], [Validators.required]]


    constructor(data?: CreateOrUpdateSitesInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }

    }
}

