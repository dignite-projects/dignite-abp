import { FormControl, Validators } from "@angular/forms";

export class NumbericEditConfig {
    /**占位符 */
    'NumericEditField.Placeholder': any = ['', []];
    /**最小值 */
    'NumericEditField.Min': any = ['', [Validators.required]];
    //最大值
    'NumericEditField.Max': any = ['', [Validators.required]];
    // 小数位数
    'NumericEditField.Decimals': any = [2, []];
    //步长
    'NumericEditField.Step': any = ['', []];
    //格式说明符
    'FormatSpecifier': any = ['', []];

    constructor(data?: NumbericEditConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }


    }
}