import { EntryConfigComponent, EntryControlComponent } from "./entry";
import { MatrixConfigComponent } from "./matrix/matrix-config.component";
import { MatrixControlComponent } from "./matrix/matrix-control.component";
import { TableConfigComponent, TableControlComponent } from "./table";

/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const FieldControlGroup: any[] = [
    {
        displayName: '表格',
        name: 'Table',
        fieldConfigComponent: TableConfigComponent,
        fieldComponent: TableControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    },
    {
        displayName: '矩阵',
        name: 'Matrix',
        fieldConfigComponent: MatrixConfigComponent,
        fieldComponent: MatrixControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    }, 
     {
        displayName: '条目',
        name: 'Entry',
        fieldConfigComponent: EntryConfigComponent,
        fieldComponent: EntryControlComponent,
        // fieldViewComponent:TextBoxViewComponent,
    },
];
