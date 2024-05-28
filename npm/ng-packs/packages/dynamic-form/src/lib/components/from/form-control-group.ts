import  { FieldControlGroupInterfaces } from "../../interfaces";
import { CkEditorConfigComponent, CkEditorControlComponent } from "./ck-editor";
import { DateEditConfigComponent } from "./date-edit/date-edit-config.component";
import { DateEditControlComponent } from "./date-edit/date-edit-control.component";
import { NumbericEditConfigComponent } from "./numeric-edit/numberic-edit-config.component";
import { NumbericEditControlComponent } from "./numeric-edit/numberic-edit-control.component";
import { SelectConfigComponent, SelectControlComponent } from "./select";
import { SwitchConfigComponent, SwitchControlComponent } from "./switch";
import { TextEditComponent, TextEditConfigComponent } from "./text-edit";

/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const FieldControlGroup: FieldControlGroupInterfaces[] = [
    {
        displayName: '文本框',
        name: 'TextEdit',
        fieldConfigComponent: TextEditConfigComponent,
        fieldComponent: TextEditComponent,
        // fieldViewComponent:TextBoxViewComponent,
    }, {
        displayName: '开关',
        name: 'Switch',
        fieldConfigComponent: SwitchConfigComponent,
        fieldComponent: SwitchControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    }, {
        displayName: '选择',
        name: 'Select',
        fieldConfigComponent: SelectConfigComponent,
        fieldComponent: SelectControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    }, {
        displayName: '数字',
        name: 'NumericEdit',
        fieldConfigComponent: NumbericEditConfigComponent,
        fieldComponent: NumbericEditControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    }, {
        displayName: '日期',
        name: 'DateEdit',
        fieldConfigComponent: DateEditConfigComponent,
        fieldComponent: DateEditControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    },

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
    return FieldControlGroup
}

export function AddFieldControlGroup(array=[]) {
    FieldControlGroup.push(...array)
}