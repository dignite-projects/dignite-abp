/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectConfig } from './select-config';
import { DfApiService } from '../../../services';
import {  moveItemInArray } from '@angular/cdk/drag-drop';

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
  _Entity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this._Entity = v;
    this.dataLoaded();
  }
  get formConfiguration() {
    return this._Entity?.get('formConfiguration') as FormGroup;
  }
  get SelectOptions() {
    return this.formConfiguration.controls['Select.Options'] as FormArray;
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
  /**增加选项 */
  addSelectOptions() {
    this.SelectOptions.push(
      new FormGroup({
        Text: new FormControl('', Validators.required),
        Value: new FormControl('', Validators.required),
        Selected: new FormControl(false),
      })
    );
  }
  /**删除某个选项 */
  deleteSelectOptions(index) {
    this.SelectOptions.removeAt(index);
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this._Entity?.setControl('formConfiguration', this.fb.group(new SelectConfig()));
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

  textChange(event, index) {
    const SelectOptionsItem = this.SelectOptions.at(index);
    const value = event.target.value;
    if (SelectOptionsItem.get('Value')?.value) return;
    SelectOptionsItem.patchValue({
      Value: this._DfApiService.chineseToPinyin(value),
    });
  }

  /**调整表格位置 */
  drop(event: any) {
    moveItemInArray(
      this.SelectOptions.controls,
      event.previousIndex,
      event.currentIndex
    );
    this.SelectOptions.updateValueAndValidity()
  }
}
