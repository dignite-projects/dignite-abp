import { FormArray} from "@angular/forms";

export class SelectConfig {
    /**空值文本 */
    'Select.NullText': any = [''];
    //多选
    'Select.Multiple': any = [false];
    // 选项
    'Select.Options': any = new FormArray([]) 
}