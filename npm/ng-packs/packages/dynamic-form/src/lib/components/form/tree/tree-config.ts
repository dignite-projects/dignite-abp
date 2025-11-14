import { FormArray, Validators} from "@angular/forms";

export class TreeConfig {
    //是否多选
    'TreeView.Multiple': any = [false];
    // 选项
    'TreeView.Nodes': any = new FormArray([]);
}
export class TreeitemConfig {
    //文本
    'Text': any = ['', [Validators.required]];
    //值
    'Value': any = ['', [Validators.required]];
    //是否选中
    'Selected': any = [false];
    //子项
    'Children': any = new FormArray([]);
}