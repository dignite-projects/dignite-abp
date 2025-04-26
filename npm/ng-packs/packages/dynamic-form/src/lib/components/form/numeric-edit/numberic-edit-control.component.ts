/* eslint-disable @angular-eslint/component-selector */
/* eslint-disable @typescript-eslint/adjacent-overload-signatures */
import {
  Component,
  ElementRef,
  Input,
  ViewChild,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AbstractControl, ValidationErrors } from '@angular/forms';

export function maxDecimalPlacesValidator(maxDecimalPlaces: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (typeof value === 'number' && isNaN(value) === false) {
      const decimalPart = value.toString().split('.')[1];
      if (decimalPart && decimalPart.length > maxDecimalPlaces) {
        return { maxDecimalPlaces: { actual: decimalPart.length, max: maxDecimalPlaces } };
      }
    }
    return null;
  };
}
@Component({
  selector: 'df-numberic-edit-control',
  templateUrl: './numberic-edit-control.component.html',
  styleUrls: ['./numberic-edit-control.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NumbericEditControlComponent {
  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) {}

  public get entity(): any {
    return this._entity;
  }

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
    //
  }

  /**表单实体 */
  _entity: FormGroup | any;

  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }

  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity?.get('extraProperties') as FormGroup;
  }
  get fieldInput() {
    return this.extraProperties.get(this._fields.field.name);
  }
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();
      this.cdr.detectChanges();
      this.submitclick.nativeElement.click();
    }
  }
  formConfiguration: any = '';
  AfterInit() {
    return new Promise((resolve, rejects) => {
      const ValidatorsArray: any[] = [];
      const formConfiguration = this._fields.field.formConfiguration;
      this.formConfiguration=formConfiguration;
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      if (formConfiguration['NumericEditField.Min']) {
        ValidatorsArray.push(Validators.min(formConfiguration['NumericEditField.Min']));
      }
      if (formConfiguration['NumericEditField.Max']) {
        ValidatorsArray.push(Validators.max(formConfiguration['NumericEditField.Max']));
      }
      const newControl = this.fb.control(this._selected, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);

      resolve(true);
    });
  }
  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.extraProperties.removeControl(this._fields.field.name)
  }

  /**数字框输入 */
  inputchange(event) {
    const val = event.target.value;

    const decimalPart = val.toString().split('.')[1] || '';
    const formConfiguration = this._fields.field.formConfiguration;
    const Decimals = formConfiguration['NumericEditField.Decimals'];
    if (decimalPart.length > Decimals) {
      this.fieldInput?.patchValue(val.slice(0, val.length - (decimalPart.length - 2)));
    }
    this.extraProperties.updateValueAndValidity();
    this.fieldInput.updateValueAndValidity();
  }
}
