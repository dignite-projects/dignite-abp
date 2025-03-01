import { FormControl, Validators } from "@angular/forms";
import { DateEditInterfaces } from "../../../enums/date-edit-interfaces";

export class DateEditConfig {
    /**日期格式 */
    'DateEdit.InputMode': any = [DateEditInterfaces.Date, []];
    /**最小值 */
    'DateEdit.Min': any = ['', []];
    /**最大值 */
    'DateEdit.Max': any = ['', []];

    constructor() {
    }
}