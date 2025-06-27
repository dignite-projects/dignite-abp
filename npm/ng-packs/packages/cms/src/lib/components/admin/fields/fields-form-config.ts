import { FormGroup, Validators } from "@angular/forms";

export class FieldsFormConfig {
    /**分组id */
    groupId: Validators[] = [''];

    /**字段名称 Display name of this field */
    displayName: Validators[] = ['', [Validators.required]];

    /**字段唯一名称 Unique Name*/
    name: Validators[] = ['', [Validators.required]];

    /**描述 说明 */
    description: Validators[] = ['', []];

    /**FieldType字段类型 表单控件名称 */
    formControlName: Validators[] = ['', [Validators.required]];

    /**动态表单配置 */
    formConfiguration: FormGroup | undefined = new FormGroup({});

}

/**
 * 字段名称与表单控件名称映射
 */
// export enum fieldToFormLabelMap{
//     /**分组id */
//     groupId = 'Cms::FieldGroup',
//     displayName = 'AbpDynamicForm::FieldDisplayName',
//     name = 'AbpDynamicForm::FieldName',
//     description='Cms::Description',
//     // Text='AbpDynamicForm::SelectListItemText',
//     // Value='AbpDynamicForm::SelectListItemValue',
//     'Select.Options'="AbpDynamicForm::SelectListItem",
//     'Select.Options-Text'="AbpDynamicForm::SelectListItemText",
//     'Select.Options-Value'="AbpDynamicForm::SelectListItemValue",
    
// }
export const fieldToFormLabelMap:any={
    /**分组id */
    groupId : 'Cms::FieldGroup',
    displayName : 'AbpDynamicForm::FieldDisplayName',
    name : 'AbpDynamicForm::FieldName',
    description:'Cms::Description',
    'Select.Options':"AbpDynamicForm::SelectListItem",
    'Select.Options-Text':"AbpDynamicForm::SelectListItemText",
    'Select.Options-Value':"AbpDynamicForm::SelectListItemValue",
    "TableColumns":'AbpDynamicForm::SelectListItem',
    "TableColumns-displayName":'Cms::TableColumnDisplayName',
    "TableColumns-name":'Cms::TableColumnName',
    "TableColumns-formControlName":'Cms::TableColumnForm',
    "MatrixBlockTypes":"Cms::MatrixBlockType",
    "fields":'Cms::Fields',
    "MatrixBlockTypes-fields-displayName":'AbpDynamicForm::FieldDisplayName',
}
