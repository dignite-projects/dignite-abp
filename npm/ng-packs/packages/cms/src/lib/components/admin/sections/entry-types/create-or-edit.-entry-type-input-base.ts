import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class CreateOrEditEntryTypeInputBase {
    /**显示名称 Display name of this field */
    displayName: any = ['', [Validators.required]];

    /**名称 Unique Name*/
    name: any = ['', [Validators.required]];
    /**条目路由 */
    fieldTabs: any = new FormArray([])
    
    constructor(data?: CreateOrEditEntryTypeInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}
export class fieldTabsBase {
    /**名称 Unique Name*/
    name: any = ['', [Validators.required]];
    fields?: any = [[], []]
    constructor(data?: fieldTabsBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}
export class fieldsBase {
    /**字段id Unique Name*/
    fieldId: any = ['', [Validators.required]];
    /**显示名称 Unique Name*/
    displayName: any = ['', [Validators.required]];
    /**必填 Unique Name*/
    required: any = [false, []];
    /**是否在列表中显示 Unique Name*/
    showOnList: any = [false, []];

    constructor(data?: fieldsBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }

    }
}

export class fieldsInputBase {
    /**字段id */
    fieldId = ['', [Validators.required]];
    /**显示名称 Display name of this field */
    displayName: any = ['', [Validators.required]];

    /**是否必填 Unique Name*/
    required: any = [false, []];
    /**在列表中展示 Unique Name*/
    showOnList: any = [false, []];
    constructor(data?: fieldsInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }

    }
}

