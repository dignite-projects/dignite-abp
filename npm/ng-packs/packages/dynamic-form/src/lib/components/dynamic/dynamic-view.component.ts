import { Component, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { FormControlService } from '../../services/form-control.service';
import { FieldControlGroupInterfaces } from '../../interfaces';

@Component({
  selector: 'df-dynamic-view',
  templateUrl: './dynamic-view.component.html',
  styleUrl: './dynamic-view.component.scss',
})
export class DynamicViewComponent {
  constructor(private _FormControlService: FormControlService) {}

  /**是否显示再列表 */
  @Input() showInList: boolean = false;
  /**表单字段数据 */
  @Input() fields: any;

  /**表单控件类型 */
  @Input() type: any;

  /**表单控件Value */
  _value: any = '';
  @Input()
  public set value(v: any) {
    this._value = v;
    // if(this._value) this.dataLoaded();
  }

  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormTemplateRef', { read: ViewContainerRef, static: true })
  FormTemplateRef?: ViewContainerRef;

  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    if (this.type && this._value!=='') {
      let _fieldControlGroup: any[] = this._FormControlService.AddFieldControlGroup();
      let fieldControlItem = _fieldControlGroup.find(el => el.name === this.type);
      this.loadViewComponent(fieldControlItem);
    }
  }

  /**加载动态展示组件 */
  loadViewComponent(FieldControlItem?: FieldControlGroupInterfaces) {
    //清空了容器中的所有组件
    this.FormTemplateRef?.clear();
    if (!FieldControlItem || !FieldControlItem.fieldViewComponent) return;
    //在容器中创建组件
    const { instance }: any = this.FormTemplateRef?.createComponent(
      FieldControlItem.fieldViewComponent,
    ); //创建组件模板
    /**向创建的组件模板中传值 */
    instance.type = this.type;
    instance.value = this._value;
    instance.fields = this.fields;
    instance.showInList = this.showInList;
  }
}
