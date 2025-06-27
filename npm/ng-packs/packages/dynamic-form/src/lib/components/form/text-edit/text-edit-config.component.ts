/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TextEditConfig } from './text-edit-config';
import { TextEditMode } from '../../../enums/text-edit-mode';

@Component({
  selector: 'df-text-edit-config',
  templateUrl: './text-edit-config.component.html',
  styleUrls: ['./text-edit-config.component.scss'],
})
export class TextEditConfigComponent {
  constructor(private fb: FormBuilder) {}
  _TextEditMode = TextEditMode;
  RadioIndex1: any = Math.floor(Math.random() * 1001);
  RadioIndex2: any = Math.floor(Math.random() * 1001);

  /**表单控件类型 */
  formControlName: any;
  @Input()
  public set type(v: any) {
    this.formControlName = v;
    this.dataLoaded();
  }
  /**表单实体 */
  formEntity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this.formEntity = v;
    this.dataLoaded();
  }
  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
    this.dataLoaded();
  }
  get formConfiguration() {
    return this.formEntity.get('formConfiguration') as FormGroup;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this.formEntity && this.formControlName) {
      await this.AfterInit();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this.formEntity.setControl('formConfiguration', this.fb.group(new TextEditConfig()));
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
      if (this._selected && this._selected.formControlName == this.formControlName) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
      }
      resolve(true);
    });
  }
}
