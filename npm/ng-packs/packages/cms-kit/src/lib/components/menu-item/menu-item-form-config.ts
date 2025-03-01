import { Validators } from "@angular/forms";
// import { AreaNamePatternValidator } from "../../services/validator";
export class menuItemFromConfig {
    /**id */
    id:string | Validators[] = [''];
    /**parentId */
    parentId:string | Validators[] = ['',[]];
    /**名称 */
    displayName:string | Validators[] = ['',[Validators.required]];
    /**活跃 */
    isActive:number | Validators[] = [true,[]];
    /**网址 */
    url:string | Validators[] = ['',[Validators.required]];
    /**图标 */
    icon:string | Validators[] = ['',[]];
    // /**排序 */
    // order:number | Validators[] = [0,[]];
    /**目标 */
    target:string | Validators[] = ['',[]];
    /**元素id */
    elementId:string | Validators[] = ['',[]];
    /**css类 */
    cssClass:string | Validators[] = ['',[]];
    // /**页面id */
    // pageId:string | Validators[] = ['',[]];





    constructor(data?: menuItemFromConfig,) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)&&this.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}

