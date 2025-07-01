import { FormGroup, Validators } from "@angular/forms";

export class CreateOrUpdateEntryInputBase {

    /**条目id */
    entryTypeId: any = ['']
    /**标题 */
    // title: any = ['', [Validators.required]]
    /**别名 */
    slug: any = ['', [Validators.required]]
    /**语言 */
    culture: any = ['']
    /**是否是草稿 */
    draft: any = ['', []]
    /**发布时间 */
    publishTime: any = ['', [Validators.required]]
    /**上级目录 */
    parentId: any = [null]
    /**修订说明 */
    versionNotes: any = [null]
    /**版本 */
    initialVersionId: any = [null]
    extraProperties: FormGroup | undefined = new FormGroup({})
}


export const EntriesToFormLabelMap={
    "title":"Cms::Title",
    "slug":"Cms::Slug",
    "culture":"Cms::Languages",
    "draft":"Cms::EntryType",
    "publishTime":"Cms::publishTime",
    "parentId":"Cms::ParentEntry",
}

