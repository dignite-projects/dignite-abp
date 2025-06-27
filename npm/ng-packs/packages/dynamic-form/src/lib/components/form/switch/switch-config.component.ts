/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SwitchConfig } from './switch-config';
@Component({
  selector: 'df-switch-config',
  templateUrl: './switch-config.component.html',
  styleUrls: ['./switch-config.component.scss'],
})
export class SwitchConfigComponent {
  constructor(private fb: FormBuilder) {}
  /**表单控件类型 */
  _type: any;
  @Input()
  public set type(v: any) {
    this._type = v;
    this.dataLoaded();
  }
  /**表单实体 */
  formEntity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup | undefined) {
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
    if (this.formEntity && this._type) {
      await this.AfterInit();
      // this.cdr.detectChanges();
    
    }
  }



  AfterInit() {
    return new Promise((resolve, rejects) => {
      this.formEntity.setControl('formConfiguration', this.fb.group(new SwitchConfig()));
       setTimeout(()=>{
      this.submitclick?.nativeElement?.click();
     },0)
      if (this._selected && this._selected.formControlName == this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
      }
      resolve(true);
    });
  }
}
