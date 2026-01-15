/* eslint-disable @angular-eslint/component-selector */
import { Component, Input } from '@angular/core';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';

@Component({
  selector: 'cms-entry-view',
  templateUrl: './entry-view.component.html',
  styleUrl: './entry-view.component.scss',
})
export class EntryViewComponent {
  constructor(private _EntryAdminService: EntryAdminService) {}
  /**展示则内容 */
  showValue: any = '';

  /**是否显示再列表 */
  @Input() showInList: boolean|any = false;
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
    if (this.type && this._value) {
      const options = this.fields?.field?.formConfiguration?.['Entry.Options'] || [];
      
      if (options.length > 0) {
        const getTextByValue = (val: any) => {
          const option = options.find((opt: any) => opt.value === val || opt.Value === val);
          return option?.text || option?.Text || val;
        };
        
        if (Array.isArray(this._value)) {
          this.showValue = this._value.map(getTextByValue).join(',');
        } else {
          this.showValue = getTextByValue(this._value);
        }
      } else if (Array.isArray(this._value)) {
        this.showValue = await this.getListByIds(this._value);
      } else {
        this.showValue = this._value;
      }
    }
  }

  /**获取条目详情 */
  getListByIds(ids: any[]) {
    return new Promise((resolve, reject) => {
      this._EntryAdminService
        .getListByIds(this.fields.field.formConfiguration['Entry.SectionId'], ids)
        .subscribe((res: any) => {
          const result = res.items
            .map((item: any) => {
              return item.slug;
            })
            .join(',');
          resolve(result);
        });
    });
  }
}
