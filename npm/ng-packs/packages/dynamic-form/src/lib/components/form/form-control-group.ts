import { FieldControlGroupInterfaces } from '../../interfaces';
// import { CkEditorConfigComponent, CkEditorControlComponent } from "./ck-editor";
import { DateEditConfigComponent } from './date-edit/date-edit-config.component';
import { DateEditControlComponent } from './date-edit/date-edit-control.component';
import { DateEditViewComponent } from './date-edit/date-edit-view.component';
import { NumbericEditConfigComponent } from './numeric-edit/numberic-edit-config.component';
import { NumbericEditControlComponent } from './numeric-edit/numberic-edit-control.component';
import { NumericEditSearchComponent } from './numeric-edit/numeric-edit-search.component';
import { NumericEditViewComponent } from './numeric-edit/numeric-edit-view.component';
import { SelectConfigComponent, SelectControlComponent, SelectSearchComponent } from './select';
import { SelectViewComponent } from './select/select-view.component';
import { SwitchConfigComponent, SwitchControlComponent } from './switch';
import { SwitchSearchComponent } from './switch/switch-search.component';
import { SwitchViewComponent } from './switch/switch-view.component';
import { TextEditComponent, TextEditConfigComponent } from './text-edit';
import { TextEditSearchComponent } from './text-edit/text-edit-search.component';
import { TextEditViewComponent } from './text-edit/text-edit-view.component';

/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const FieldControlGroup: FieldControlGroupInterfaces[] = [
  {
    displayName: '文本框',
    name: 'TextEdit',
    fieldConfigComponent: TextEditConfigComponent,
    fieldComponent: TextEditComponent,
    fieldViewComponent:TextEditViewComponent,
    fieldSearchComponent:TextEditSearchComponent,
  },
  {
    displayName: '开关',
    name: 'Switch',
    fieldConfigComponent: SwitchConfigComponent,
    fieldComponent: SwitchControlComponent,
    fieldViewComponent:SwitchViewComponent,
    fieldSearchComponent:SwitchSearchComponent,
  },
  {
    displayName: '选择',
    name: 'Select',
    fieldConfigComponent: SelectConfigComponent,
    fieldComponent: SelectControlComponent,
    fieldViewComponent:SelectViewComponent,
    fieldSearchComponent:SelectSearchComponent,
  },
  {
    displayName: '数字',
    name: 'NumericEdit',
    fieldConfigComponent: NumbericEditConfigComponent,
    fieldComponent: NumbericEditControlComponent,
    fieldViewComponent:NumericEditViewComponent,
    fieldSearchComponent:NumericEditSearchComponent,
  },
  {
    displayName: '日期',
    name: 'DateEdit',
    fieldConfigComponent: DateEditConfigComponent,
    fieldComponent: DateEditControlComponent,
    fieldViewComponent:DateEditViewComponent,
  },
];


export function addFieldControlGroup(array:any[] = []) {

  for (const element of array) {
    const find = FieldControlGroup.find((control) => {
      return control.name === element.name;
    });
    if(!find){
      FieldControlGroup.push(element);
    }
  }
  return FieldControlGroup;
}
