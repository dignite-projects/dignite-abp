import { ChangeDetectorRef, Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CmsApiService } from '../../../services';
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'df-table-control',
  templateUrl: './table-control.component.html',
  styleUrls: ['./table-control.component.scss'],
})
export class TableControlComponent {
  constructor(
    private _CmsApiService: CmsApiService // private fb: FormBuilder,
  ) {}
  private fb = inject(FormBuilder);

  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    if (v) {
      for (const key in v.field?.formConfiguration) {
        if (Array.isArray(v.field?.formConfiguration[key])) {
          v.field.formConfiguration[key] = this._CmsApiService.convertKeysToCamelCase(
            v.field?.formConfiguration[key]
          );
        }
      }
      this._fields = v;
      this.dataLoaded();
    }
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
    this.dataLoaded();
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    if (v) {
      v = this._CmsApiService.convertKeysToCamelCase(v);
      this._selected = v;
      this.dataLoaded();
    }
  }
  /**语言 */
  _culture: any;
  @Input()
  public set culture(v: any) {
    this._culture = v;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /** 获取父级字段代表的表单组*/
  extraProperties: FormGroup | undefined;
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName) {
      this.extraProperties = this._entity.get(this._parentFiledName) as FormGroup;
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  formConfiguration: any;
  /**获取表格字段代表的控件 */
  fieldNameControl: FormArray | undefined;

  AfterInit() {
    return new Promise((resolve, rejects) => {
      const ValidatorsArray = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      const formConfiguration = this._fields.field.formConfiguration;
     
      this.formConfiguration = formConfiguration;
      const newArrayGroup = this.fb.array([],ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newArrayGroup);
      this.fieldNameControl = this.extraProperties.get(this._fields.field.name) as FormArray;
      if(this._fields.required){
        this.fieldNameControl.addValidators(this.hasValueValidator);
        // for (const element of formConfiguration.TableColumns) {
        //   element.required = true;
        // }
      }
      if (this._selected) {
        this._selected.forEach(el => {
          this.addTableControlItem();
        });
        this.fieldNameControl.patchValue(this._selected);
      } else {
        this.addTableControlItem();
      }
      resolve(true);
    });
  }
 /**是否带有值验证器 */
 hasValueValidator(g: FormArray) {
  // 检查是否有任何控件包含有效值
  const hasValue = g.controls.some(element => {
    const extraProperties = element.get('extraProperties');
    return Object.keys(extraProperties.value).some(key => {
      const value = extraProperties.value[key];
      // 检查数组类型值
      if (Array.isArray(value)) {
        return value.length > 0;
      }
      // 检查非空值
      return value !== null && value !== undefined && value !== '';
    });
  });

  // 根据验证结果设置表单状态
  g.controls.forEach(element => {
    const extraProperties = element.get('extraProperties');
    Object.keys(extraProperties.value).forEach(key => {
      if (!hasValue) {
        // 如果没有值，设置所有字段为必填
        extraProperties.get(key).setValidators(Validators.required);
        extraProperties.get(key).setErrors({ required: true });
      } else {
        // 如果有值，清除错误
        extraProperties.get(key).setErrors(null);
      }
    });
  });

  // 返回验证结果
  return hasValue&&g.controls.length>0 ? null : { required: true };
}

  /**增加表格项 */
  addTableControlItem() {
    this.fieldNameControl.push(
      this.fb.group({
        extraProperties: this.fb.group({}),
      })
    );
    // this.fieldNameControl.updateValueAndValidity();
  }
  /**删除表格项 */
  minusTableControlItem(index) {
    this.fieldNameControl.removeAt(index);
  }
  /**调整表格位置 */
  TableArrowUpOrDown(type, index) {
    const controlAt = this.fieldNameControl.at(index);
    this.fieldNameControl.removeAt(index);
    const lastindex = type == 'up' ? index - 1 : index + 1;
    this.fieldNameControl.insert(lastindex, controlAt);
    this._selected = this.fieldNameControl.value;
  }
  /**调整表格位置 */
  drop(event: any) {
    moveItemInArray(this.fieldNameControl.controls, event.previousIndex, event.currentIndex);
    this.fieldNameControl.updateValueAndValidity();
  }
}
