/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NumbericEditConfig } from './numberic-edit-config';

@Component({
  selector: 'df-numberic-edit-config',
  templateUrl: './numberic-edit-config.component.html',
  styleUrls: ['./numberic-edit-config.component.scss'],
})
export class NumbericEditConfigComponent {
  constructor(private fb: FormBuilder) {}
  /**表单控件类型 */
  _type: any;
  @Input()
  public set type(v: any) {
    this._type = v;
    //  this.dataLoaded()
  }

  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v ? v : v == false ? v : '';
  }
  /**表单实体 */
  formEntity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this.formEntity = v;
    this.dataLoaded();
  }
  get formConfiguration() {
    return this.formEntity.get('formConfiguration') as FormGroup;
  }
  @ViewChild('submitclick', { static: false }) submitclick?: ElementRef;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this.formEntity && this._type) {
      await this.AfterInit();
     
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this.formEntity.setControl('formConfiguration', this.fb.group(new NumbericEditConfig()));
       this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
      if (this._selected && this._selected.formControlName == this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
      }
      resolve(true);
    });
  }
}
