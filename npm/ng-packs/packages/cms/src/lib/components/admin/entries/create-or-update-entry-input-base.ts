import { FormGroup, Validators } from "@angular/forms";

export class CreateOrUpdateEntryInputBase {
    /**语言 */
    culture: any = ['', [Validators.required]]
    /**条目id */
    entryTypeId: any = ['']
    /**标题 */
    title: any = ['', [Validators.required]]
    /**别名 */
    slug: any = ['', [Validators.required]]
    /**是否是草稿 */
    draft: any = ['', []]
    /**发布时间 */
    publishTime: any = ['', [Validators.required]]
    /**上级目录 */
    parentId:any=[null]
    /**修订说明 */
    versionNotes:any=[null]
    /**版本 */
    initialVersionId: any = [null]
    extraProperties: FormGroup | undefined = new FormGroup({})
    constructor(data?: CreateOrUpdateEntryInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}

