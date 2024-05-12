import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CkEditorConfig } from './ck-editor-config';

@Component({
  selector: 'df-ck-editor-config',
  templateUrl: './ck-editor-config.component.html',
  styleUrls: ['./ck-editor-config.component.scss']
})
export class CkEditorConfigComponent {
  constructor(
    private fb: FormBuilder,
  ) { }
  /**表单实体 */
  @Input() Entity: FormGroup | undefined

  /**选择的表单信息 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v ||''

    this.dataLoaded()
  }
  /**表单控件类型 */
 _type: any
 @Input()
 public set type(v: any) {
   this._type = v
   this.dataLoaded()
 }
  get formConfiguration() {
    return this.Entity.get('formConfiguration') as FormGroup
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  async dataLoaded() {
    // if (this.Entity && (this._selected || this._selected === '')) {
      if (this.Entity && this._type){
      await this.AfterInit()
      // this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this.Entity.setControl('formConfiguration', this.fb.group(new CkEditorConfig()))
      if (this._selected&&this._selected.formControlName==this._type) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration
        })
      }
      resolve(true)
    })
  }
}
