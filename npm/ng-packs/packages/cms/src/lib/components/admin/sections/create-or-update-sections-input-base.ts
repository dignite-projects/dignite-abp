
import {Validators } from "@angular/forms";

export class CreateOrUpdateSectionsInputBase {
    id: string | Validators[] = [''];
    /**版块类型 */
    type: any = [0, [Validators.required]];
    /**字段名称 Display name of this field */
    displayName: any = ['', [Validators.required]];
    /**字段唯一名称 Unique Name*/
    name: any = ['', [Validators.required]];
    /**条目路由 */
    route: any = ['', [Validators.required]];
    /**页面模板 */
    template: any = ['', [Validators.required]];
    /**是否默认 */
    isDefault: any = [false, []];
    /**是否激活 */
    isActive: any = [true, []];
    concurrencyStamp: string | Validators[] = [''];

    constructor() {}
}