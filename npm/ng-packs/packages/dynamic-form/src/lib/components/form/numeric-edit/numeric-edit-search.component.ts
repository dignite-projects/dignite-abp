/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'df-numeric-edit-search',
  templateUrl: './numeric-edit-search.component.html',
  styleUrl: './numeric-edit-search.component.scss',
})
export class NumericEditSearchComponent {
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
  /**定义动态字符 */
  get numberInput() {
    return this.extraProperties.get(this._fields.field.name) as FormControl;
  }
  /**定义number表单用于获取最小值最大值 */
  numberForm: FormGroup = new FormGroup({
    min: new FormControl(''),
    max: new FormControl(''),
  });
  get minInput() {
    return this.numberForm.get('min') as FormControl;
  }
  get maxInput() {
    return this.numberForm.get('max') as FormControl;
  }

  formConfiguration: any = '';
  AfterInit() {
    return new Promise(resolve => {
      const ValidatorsArray: any[] = [];
      this.formConfiguration = this._fields.field.formConfiguration;

      const newControl = this.fb.control(this._selected, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);
      this.numberForm.valueChanges.subscribe(res => {
        if (res.min<Number(this.formConfiguration['NumericEditField.Min'])) {
          this.minInput.patchValue(this.formConfiguration['NumericEditField.Min']);
        }
        if (res.min>Number(this.formConfiguration['NumericEditField.Max'])) {
          this.minInput.patchValue(this.formConfiguration['NumericEditField.Max']);
        }
        if ((res.min>res.max)&&res.max) {
          this.minInput.patchValue(res.max);
        }
        if (res.max>Number(this.formConfiguration['NumericEditField.Max'])) {
          this.maxInput.patchValue(this.formConfiguration['NumericEditField.Max']);
        }
        if (this.numberForm.valid && res.min && res.max) {
          this.numberInput.patchValue(`${res.min}-${res.max}`);
        } else {
          this.numberInput.patchValue('');
        }
      });
      resolve(true);
    });
  }

  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.extraProperties.removeControl(this._fields.field.name);
  }
}
