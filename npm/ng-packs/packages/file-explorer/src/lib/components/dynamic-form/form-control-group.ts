// import { TableConfigComponent, TableControlComponent, MatrixConfigComponent, MatrixControlComponent } from "@dignite-ng/expand.cms";
import { Type } from "@angular/core";
import { FileExplorerConfigComponent, FileExplorerControlComponent } from "./file-explorer";
import { FileExplorerViewComponent } from "./file-explorer/file-explorer-view.component";

export interface FieldControlGroupInterfaces {
    displayName: string
    name: string
    // type: TextEditMode,
    /**
 * 动态表单控件
 */
    fieldComponent?: Type<any>;

    /**
     * 表单控件配置组件
     */
    fieldConfigComponent?: Type<any>;

    /**
     * 条目列表中显示的组件
     */
    fieldViewComponent?: Type<any>;
}



/**
 * 表单控件分组-包含配置，控件，显示的数组
 */
export const fielFieldControlGroup: FieldControlGroupInterfaces[] = [
   
     {
        displayName: '文件管理',
        name: 'FileExplorer',
        fieldConfigComponent: FileExplorerConfigComponent,
        fieldComponent: FileExplorerControlComponent,
        fieldViewComponent:FileExplorerViewComponent,
    }
];
export function getExcludeAssignControl(typeName) {
    // return FieldControlGroup.filter(el => el.name !== typeName)
    return fielFieldControlGroup
}

export function AddFieldControlGroup(array=[]) {
    fielFieldControlGroup.push(...array)
}