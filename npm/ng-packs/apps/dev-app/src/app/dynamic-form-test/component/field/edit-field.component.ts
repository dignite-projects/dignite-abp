import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { Component, ElementRef, ViewChild, inject } from '@angular/core';
import { DynamicComponent } from '../../enums';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services';
import { Location } from '@angular/common';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { FieldDataService } from '../../services/field-data.service';

@Component({
  selector: 'app-edit-field',
  templateUrl: './edit-field.component.html',
  styleUrl: './edit-field.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: DynamicComponent.FieldsEdit,
    },
  ]
})
export class EditFieldComponent {

  constructor(
  ) { }
  private fb = inject(FormBuilder)
  private route = inject(ActivatedRoute)
  private _FieldDataService = inject(FieldDataService)
  private _location = inject(Location)
  private _LocalizationService = inject(LocalizationService)
  private _ApiService = inject(ApiService)
  private router = inject(Router)
  private toaster = inject(ToasterService)


  /**表单实体 */
  newEntity: FormGroup | undefined

  /**字段id */
  fieldId: string = '';

  /**字段详情 */
  fieldDetails: any;

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;


  ngOnInit() {
    const _fieldId = this.route.snapshot.params.id;
    if (_fieldId) {
      this.fieldId = _fieldId;
      this.newEntity = this.fb.group(new CreateOrUpdateFieldInputBase());
      this._FieldDataService.getFieldId(_fieldId).subscribe(res => {
        this.fieldDetails = res
        this.newEntity.patchValue({
          ...this.fieldDetails,
        });
      })
    }
  }



  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click()
  }

  /**保存表单 */
  save() {
    let input = this.newEntity.value
    if (!this.newEntity.valid) return
    console.log(input,'保存表单');
    this._FieldDataService.editField(this.fieldId,input).subscribe(()=>{
      this._location.back()
    })
  }
}
