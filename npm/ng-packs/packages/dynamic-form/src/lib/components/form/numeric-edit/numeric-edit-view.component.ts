/* eslint-disable @angular-eslint/component-selector */
import { Component, Input, AfterContentInit } from '@angular/core';

@Component({
  selector: 'df-numeric-edit-view',
  templateUrl: './numeric-edit-view.component.html',
  styleUrl: './numeric-edit-view.component.scss',
})
export class NumericEditViewComponent implements AfterContentInit {
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
      this.showValue = valueOptions;
    }
  }
}
