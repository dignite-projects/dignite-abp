/* eslint-disable @angular-eslint/use-lifecycle-interface */

import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { DatePipe, Location } from '@angular/common';
import { Component, OnInit, inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
// import { EntryAdminService } from '../../../proxy/admin/entries';
import { ECmsComponent } from '../../../enums';
import { UpdateListService } from '@dignite-ng/expand.core';
import { CreateOrUpdateEntryInputBase } from './create-or-update-entry-input-base';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';

@Component({
  selector: 'cms-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Entries_Edit,
    },
  ],
})
export class EditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private _updateListService = inject(UpdateListService);
  private toaster = inject(ToasterService);
  public _location = inject(Location);
  private route = inject(ActivatedRoute);
  private _EntryAdminService = inject(EntryAdminService);
  private _LocalizationService = inject(LocalizationService);
  private _ValidatorsService = inject(ValidatorsService);
  private datePipe = inject(DatePipe);

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';
  /**表单实体 */
  formEntity: FormGroup | undefined;
  /**语言 */
  cultureName: string = '';
  /**条目类型id */
  entryTypeId: string = '';
  /**版块id */
  sectionId: string = '';
  /**条目版本id */
  entryVersionId: string = '';
  /**条目id */
  entrieId: string = '';
  /**条目信息 */
  entryInfo: any = '';

  /**是否草稿控件*/
  get draftInput() {
    return this.formEntity?.get('draft') as FormControl;
  }
  /**语言控件 */
  get cultureInput(): FormControl {
    return this.formEntity?.get('culture') as FormControl;
  }

  async ngOnInit(): Promise<void> {
    let params = this.route.snapshot.params;
    this.entrieId = params.entrieId;
    this.formEntity = this.fb.group(new CreateOrUpdateEntryInputBase());
    await this.getEntryInfo();
    this.cultureInput.patchValue(this.cultureName);

  }
  /**获取条目信息 */
  getEntryInfo() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.get(this.entrieId).subscribe(res => {
        this.cultureName = res.culture;
        this.entryTypeId = res.entryTypeId;
        this.sectionId = res.sectionId;
        this.entryInfo = res;
        resolve(res);
      });
    });
  }

  /**提交 */
  save() {
    let input = this.formEntity?.value;
    input.culture = this.cultureName;
    input.publishTime = new Date(
      new Date(input.publishTime).getTime() + 8 * 60 * 60 * 1000
    ).toISOString();
    input.concurrencyStamp = this.entryInfo.concurrencyStamp;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.formEntity);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')){
      this.isSubmit = false;
      return this.cultureInput.disable();
    }
    
    if (!this.formEntity.valid) return;
    this._EntryAdminService
      .update(this.entryInfo.id, input)
      .pipe(finalize(() => {
        this.isSubmit = false;
      }))
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        this.backTo();
        this._updateListService.updateList();
      });
  }
  isSubmit: boolean = false;
  /**点击提交 */
  clickSubmit(type) {
    if (this.isSubmit) return;
    this.isSubmit = true;
    this.draftInput.patchValue(type);
    this.cultureInput.enable();
    this.submitclick?.nativeElement?.click();
  }

  /**返回上一页 */
  backTo() {
    this._location.back();
  }
}
