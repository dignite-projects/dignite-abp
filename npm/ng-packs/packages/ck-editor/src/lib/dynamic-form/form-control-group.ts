import { FieldControlGroupInterfaces } from "../interfaces";
import { CkEditorConfigComponent, CkEditorControlComponent } from "./ck-editor";

/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const ckEditorFieldControlGroup: FieldControlGroupInterfaces[] = [
     {
        displayName: 'CkEditor',
        name: 'CkEditor',
        fieldConfigComponent: CkEditorConfigComponent,
        fieldComponent: CkEditorControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    },
];
export function getExcludeAssignControl(typeName?) {
    // return FieldControlGroup.filter(el => el.name !== typeName)
    return ckEditorFieldControlGroup
}

export function AddFieldControlGroup(array=[]) {
    ckEditorFieldControlGroup.push(...array)
}