import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder,  FormGroup } from '@angular/forms';
import { SwitchConfig } from './switch-config';
@Component({
  selector: 'df-switch-config',
  templateUrl: './switch-config.component.html',
  styleUrls: ['./switch-config.component.scss']
})
export class SwitchConfigComponent {
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
    this._selected = v

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
      this._Entity.setControl('formConfiguration', this.fb.group(new SwitchConfig()))
      if (this._selected&&this._selected.formControlName==this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration
        })
      }
      resolve(true)
    })
  }

}
