import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { DateEditConfig } from './date-edit-config';
import { DateEditInterfaces } from '../../../enums/date-edit-interfaces';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'df-date-edit-config',
  templateUrl: './date-edit-config.component.html',
  styleUrls: ['./date-edit-config.component.scss'],
})
export class DateEditConfigComponent {
  constructor(private fb: FormBuilder) {}
  private _dataPipe = inject(DatePipe);
  _DateEditInterfaces = DateEditInterfaces;
  /**表单控件类型 */
  _type: any;
  @Input()
  public set type(v: any) {
    this._type = v;
  }

  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }
  /**表单实体 */
  _Entity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this._Entity = v;
    this.dataLoaded();
  }

  dateTimeType: any = 'date';
  get formConfiguration() {
    return this._Entity.get('formConfiguration') as FormGroup;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._Entity && this._type) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this._Entity.setControl('formConfiguration', this.fb.group(new DateEditConfig()));
      if (this._selected && this._selected.formControlName == this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
        this.timeTypeChange()
      }
      resolve(true);
    });
  }
  /**切换时间类型 */
  timeTypeChange() {
    let type = this.formConfiguration.value['DateEdit.InputMode'];
    let Min = this.formConfiguration.value['DateEdit.Min'];
    let Max = this.formConfiguration.value['DateEdit.Max'];
    if (type == DateEditInterfaces.Date) {
      this.dateTimeType = 'date';
      Min = this._dataPipe.transform(Min, 'yyyy-MM-dd');
      Max = this._dataPipe.transform(Max, 'yyyy-MM-dd');
    } else if (type == DateEditInterfaces.DateTime) {
      this.dateTimeType = 'datetime-local';
      Min = this._dataPipe.transform(Min, 'yyyy-MM-dd HH:mm:ss');
      Max = this._dataPipe.transform(Max, 'yyyy-MM-dd HH:mm:ss');
    } else if (type == DateEditInterfaces.Month) {
      this.dateTimeType = 'month';
      Min = this._dataPipe.transform(Min, 'yyyy-MM');
      Max = this._dataPipe.transform(Max, 'yyyy-MM');
    }
    this.formConfiguration.patchValue({
      'DateEdit.Min': Min,
      'DateEdit.Max': Max,
    });
  }
}
