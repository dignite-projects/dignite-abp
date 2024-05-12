import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FieldAbstractsService } from '../../../services/field-abstracts.service';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { ActivatedRoute, Router } from '@angular/router';
import { FieldAdminService } from '../../../proxy/admin/fields';
import { ToasterService } from '@abp/ng.theme.shared';
import { Location } from '@angular/common';
import { LocalizationService } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums';
import { CmsApiService } from '../../../services';
import { UpdateListService } from '../../../services/update-list.service';

@Component({
  selector: 'cms-edit-field',
  templateUrl: './edit-field.component.html',
  styleUrls: ['./edit-field.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsEdit,
    },
  ]
})
export class EditFieldComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    public _FieldAbstractsService: FieldAbstractsService,
    public _FieldAdminService: FieldAdminService,
    private route: ActivatedRoute,
    private toaster: ToasterService,
    public _location: Location,
    public _LocalizationService: LocalizationService,
    public _CmsApiService: CmsApiService,
    private router: Router,
  ) { }

  /**表单实体 */
  newEntity: FormGroup | undefined

  /**字段id */
  fieldId: string = '';

  /**字段详情 */
  fieldDetails: any;

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;


  async ngOnInit(): Promise<void> {
    const _fieldId = this.route.snapshot.params.id;
    if (_fieldId) {
      this.fieldId = _fieldId;
      this.newEntity = this.fb.group(new CreateOrUpdateFieldInputBase());
      await Promise.all([
        this._FieldAbstractsService.getFromControlList(),
        this.getFieldEdit(),
      ]);
      this.newEntity.patchValue({
        ...this.fieldDetails,
      });
    }
  }

  /**获取字段详情 */
  getFieldEdit() {
    return new Promise((resolve, reject) => {
      this._FieldAdminService.get(this.fieldId).subscribe(res => {
        res.groupId = res.groupId ? res.groupId : ''
        this.fieldDetails = res
        resolve(res)
      })
    })
  }

  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click()
  }
  private _UpdateListService=inject(UpdateListService)

  /**保存表单 */
  save() {
    let input = this.newEntity.value
    if (!this.newEntity.valid) return
    this._FieldAdminService.update(this.fieldId, input).subscribe((res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
        this.router.navigate([`/cms/admin/fields`]);
        this._UpdateListService.updateList();
      });
    }))
  }

  

}
