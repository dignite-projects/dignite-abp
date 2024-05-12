import { Component, ElementRef, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NumbericEditConfig } from './numberic-edit-config';

@Component({
  selector: 'df-numberic-edit-config',
  templateUrl: './numberic-edit-config.component.html',
  styleUrls: ['./numberic-edit-config.component.scss']
})
export class NumbericEditConfigComponent {
  constructor(
    private fb: FormBuilder,
  ) { }
 /**表单控件类型 */
 _type: any
 @Input()
 public set type(v: any) {
   this._type = v
   this.dataLoaded()
 }
 /**表单实体 */
 _Entity: FormGroup | undefined
 @Input()
 public set Entity(v: FormGroup) {
   this._Entity = v;
   this.dataLoaded()
 }
  /**选择的表单信息 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v?v:v==false?v:''

    this.dataLoaded()
  }
  get formConfiguration() {
    return this._Entity.get('formConfiguration') as FormGroup
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  async dataLoaded() {
    if (this._Entity && this._type) {
      await this.AfterInit()
      this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this._Entity.setControl('formConfiguration', this.fb.group(new NumbericEditConfig()))
      if (this._selected&&this._selected.formControlName==this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration
        })
      }
      resolve(true)
    })
  }
}
