import { FieldControlGroupInterfaces } from "../interfaces";
import { CkEditorConfigComponent, CkEditorControlComponent } from "./ck-editor";
import { CkEditorViewComponent } from "./ck-editor/ck-editor-view.component";

/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const ckEditorFieldControlGroup: FieldControlGroupInterfaces[] = [
     {
        displayName: 'CkEditor',
        name: 'CkEditor',
        fieldConfigComponent: CkEditorConfigComponent,
        fieldComponent: CkEditorControlComponent,
        fieldViewComponent:CkEditorViewComponent,
    },
];
export function getExcludeAssignControl(typeName?) {
    // return FieldControlGroup.filter(el => el.name !== typeName)
    return ckEditorFieldControlGroup
}

export function AddFieldControlGroup(array=[]) {
    ckEditorFieldControlGroup.push(...array)
}