import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'df-select-control',
  templateUrl: './select-control.component.html',
  styleUrls: ['./select-control.component.scss'],
})
export class SelectControlComponent {
  constructor(private fb: FormBuilder) {}

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }

  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity?.get('extraProperties') as FormGroup;
  }
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }
 
  formConfiguration: any = '';
  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray:any[] = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      this.formConfiguration = this._fields.field.formConfiguration;
      if (!this._selected) {
        const isMultiple = this.formConfiguration['Select.Multiple'];

        let selectValue:any = isMultiple ? [] : '';
        this.formConfiguration['Select.Options'].forEach(el => {
          if (el.Selected) {
            selectValue = isMultiple
              ? [...selectValue, el.value || el.Value]
              : [el.value || el.Value];
          }
        });

        this._selected = selectValue;
      }
      let newControl = this.fb.control(this._selected, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);
      resolve(true);
    });
  }
  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.extraProperties.removeControl(this._fields.field.name);
  }
}
