import { Type } from "@angular/core";

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

