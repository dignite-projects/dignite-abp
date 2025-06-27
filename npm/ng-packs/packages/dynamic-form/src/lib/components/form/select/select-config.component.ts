/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectConfig } from './select-config';
import { DfApiService } from '../../../services';
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'df-select-config',

  templateUrl: './select-config.component.html',
  styleUrls: ['./select-config.component.scss'],
})
export class SelectConfigComponent {
  constructor(private fb: FormBuilder, private _DfApiService: DfApiService) {}
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
  formEntity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this.formEntity = v;
    this.dataLoaded();
  }
  get formConfiguration() {
    return this.formEntity?.get('formConfiguration') as FormGroup;
  }
  get SelectOptions() {
    return this.formConfiguration.controls['Select.Options'] as FormArray;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this.formEntity && this._type) {
      await this.AfterInit();
    }
  }
  /**增加选项 */
  addSelectOptions() {
    this.SelectOptions.push(
      new FormGroup({
        Text: new FormControl('', Validators.required),
        Value: new FormControl('', Validators.required),
        Selected: new FormControl(false),
      }),
    );
  }
  /**删除某个选项 */
  deleteSelectOptions(index) {
    this.SelectOptions.removeAt(index);
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this.formEntity?.setControl('formConfiguration', this.fb.group(new SelectConfig()));
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
      if (this._selected && this._selected.formControlName == this._type) {
        for (const element of this._selected.formConfiguration['Select.Options']) {
          for (const key in element) {
            const item = element[key];
            const capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1);
            element[capitalizedKey] = item;
          }
          this.addSelectOptions();
        }
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
      } else {
        this.addSelectOptions();
      }
      resolve(true);
    });
  }

  /**
   * 当选择项的文本发生变化时，更新对应的值
   * @param event 输入事件对象
   * @param index 选择项的索引位置
   * @description 如果选择项已有Value值则不处理，否则将中文文本转换为拼音作为Value值
   */
  textChange(event, index) {
    const SelectOptionsItem = this.SelectOptions.at(index);
    const value = event.target.value;
    if (SelectOptionsItem.get('Value')?.value) return;
    SelectOptionsItem.patchValue({
      Value: structuredClone(value),
    });
  }

  /**调整表格位置 */
  drop(event: any) {
    moveItemInArray(this.SelectOptions.controls, event.previousIndex, event.currentIndex);
    this.SelectOptions.updateValueAndValidity();
  }
}
