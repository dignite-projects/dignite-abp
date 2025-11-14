/* eslint-disable @angular-eslint/component-selector */
import { Component, Input } from '@angular/core';

@Component({
  selector: 'df-select-view',
  templateUrl: './select-view.component.html',
  styleUrl: './select-view.component.scss',
})
export class SelectViewComponent {
  /**展示则内容 */
  showValue: any = '';

  /**是否显示再列表 */
  @Input() showInList = false;
  /**表单字段数据 */
  @Input() fields: any;

  /**表单控件类型 */
  @Input() type: any;

  /**表单控件Value */
  _value: any = '';
  @Input()
  public set value(v: any) {
    this._value = v;
  }

  async ngAfterContentInit(): Promise<void> {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    const valueOptions = this._value;
   
    if (this.type && valueOptions) {
      const options = this.fields?.field?.formConfiguration?.['Select.Options'] || [];
      const getTextByValue = (val: any) => {
        const option = options.find((opt: any) => opt.value === val || opt.Value === val);
        return option?.text || option?.Text || val;
      };
      
      if (Array.isArray(valueOptions)) {
        this.showValue = valueOptions.map(getTextByValue).join(',');
      } else {
        this.showValue = getTextByValue(valueOptions);
      }
    }
  }
}
