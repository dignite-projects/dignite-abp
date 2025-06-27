/* eslint-disable @angular-eslint/component-selector */
/* eslint-disable @angular-eslint/use-lifecycle-interface */

import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import {  Location } from '@angular/common';
import { Component, OnInit, inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
// import { EntryAdminService } from '../../../proxy/admin/entries';
import { ECmsComponent } from '../../../enums';
import { DigniteValidatorsService, LocationBackService, UpdateListService } from '@dignite-ng/expand.core';
import { CreateOrUpdateEntryInputBase, EntriesToFormLabelMap } from './create-or-update-entry-input-base';
// import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';
// import { ClassicEditor, Essentials, Paragraph, Bold, Italic } from 'ckeditor5';

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
  // private _ValidatorsService = inject(ValidatorsService);
  // private datePipe = inject(DatePipe);

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
  cultureName: string | any = '';
  /**条目类型id */
  entryTypeId: string | any = '';
  /**版块id */
  sectionId: string | any = '';
  /**条目版本id */
  entryVersionId: string | any = '';
  /**条目id */
  entrieId: string | any = '';
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
    const params = this.route.snapshot.params;
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

  /**显示条目类型信息 */
  showEntryTypeInfo: any = '';
  /**反馈子级页面信息 */
  _feedbackChildInfo(event) {
    this.showEntryTypeInfo = event?.showEntryType || '';
  }
  // /**当返回结果为true时表示未通过验证 */
  isCheckFormCms(input, module) {
    for (const key in input) {
      if (input[key] === false) {
        let info = ``;
        //检查key中是否含有ExtraProperties.
        if (key.includes('extraProperties.')) {
          const arr = key.split('.');
          const keyName = arr[1];
          // if (keyName.includes('[')) {
          //   //使用正则提取keyName中[]中的数字下标,并且转化为数字类型，并且去掉keyName中的[*]
          //   const keyNameArr = keyName.match(/\d+/g);
          //   const keyNameArrNum = keyNameArr.map(item => Number(item));
          //   const keyNameArrNumStr = keyNameArrNum.join('.');

          // } else {
            //将keyName的首字母转为小写
            const keyNameLower = keyName;
            // const keyNameLower = keyName.charAt(0).toLowerCase() + keyName.slice(1);
            if (this.showEntryTypeInfo && this.showEntryTypeInfo.fieldTabs.length > 0) {
              for (const item of this.showEntryTypeInfo.fieldTabs) {
                for (const el of item.fields) {
                  if (el.field.name == keyNameLower) {
                    // info = `"${this._LocalizationService.instant(`${module}::${item.name}下的${el.field.displayName}字段`)}"`;
                    info = `${this._LocalizationService.instant(
                      `${module}::The{1}FieldUnderThe{0}TAB`,
                      item.name,
                      el.field.displayName,
                    )}`;
                  }
                }
              }
            }
          // }
        } else {
          const displayName = key.charAt(0).toUpperCase() + key.slice(1);
          info = `"${this._LocalizationService.instant(`${module}::${displayName}`)}" `;
        
        }
        console.log('info', info,input[key]);
        info = info + this._LocalizationService.instant(`AbpValidation::ThisFieldIsNotValid.`);
        //使用abp多语言提示
        this.toaster.warn(info);
        return true;
      }
    }

    return false;
  }
  // /**获取表单所有字段是否通过验证 */
  // getFormValidationStatus(formEntity: FormGroup | FormArray): { [key: string]: any } {
  //   const validationStatus: { [key: string]: any } = {};

  //   // 递归遍历表单组和表单控件集合
  //   const traverseForm = (form: FormGroup | FormArray, prefix = '') => {
  //     if (form instanceof FormGroup) {
  //       Object.keys(form.controls).forEach(key => {
  //         const control = form.controls[key];
  //         // const displayName = key.charAt(0).toUpperCase() + key.slice(1);
  //         const displayName = key;
  //         const fullKey = prefix ? `${prefix}.${displayName}` : displayName;
  //         if (control instanceof FormControl) {
  //           validationStatus[fullKey] = control.valid;
  //         } else if (control instanceof FormArray) {
  //           traverseForm(control, fullKey);
  //         } else if (control instanceof FormGroup) {
  //           traverseForm(control, fullKey);
  //         }
  //       });
  //     } else if (form instanceof FormArray) {
  //       form.controls.forEach((control, index) => {
  //         const fullKey = prefix ? `${prefix}[${index}]` : `[${index}]`;
  //         if (control instanceof FormControl) {
  //           validationStatus[fullKey] = control.valid;
  //         } else if (control instanceof FormArray) {
  //           traverseForm(control, fullKey);
  //         } else if (control instanceof FormGroup) {
  //           traverseForm(control, fullKey);
  //         }
  //       });
  //     }
  //   };

  //   traverseForm(formEntity);

  //   return validationStatus;
  // }

  /**获取表单验证状态 */
  // getFormValidationStatus(formEntity: FormGroup | FormArray): { [key: string]: any } {
  //   const validationStatus: { [key: string]: any } = {};

  //   const traverseForm = (form: FormGroup | FormArray, prefix = '') => {
  //     //检查form是否为FormGroup还是formArray,如果是FormGroup则继续遍历,如果是FormArray则遍历FormArray
  //     if (form instanceof FormGroup) {
        
  //     } else if (form instanceof FormArray) {
        
  //     } 

  //   }
  //   traverseForm(formEntity);
  //   return validationStatus
  // }
private _LocationBackService=inject(LocationBackService);
private _DigniteValidatorsService=inject(DigniteValidatorsService);
  /**提交 */
  save() {
    this.cultureInput.enable();
    this.draftInput.patchValue(this.draftValue || false);
    const input = this.formEntity?.value;
    input.culture = this.cultureName;
    input.publishTime = new Date(
      new Date(input.publishTime).getTime() + 8 * 60 * 60 * 1000,
    ).toISOString();
    input.concurrencyStamp = this.entryInfo.concurrencyStamp;
    // this.formValidation = this._ValidatorsService.getFormValidationStatus(this.formEntity);
    // if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')){
    //   this.isSubmit = false;
    //   return this.cultureInput.disable();
    // }

    // this.formValidation = this.getFormValidationStatus(this.formEntity);
    // if (this.isCheckFormCms(this.formValidation, 'Cms')) {
    //   this.isSubmit = false;
    //   return this.cultureInput.disable();
    // }
    // this.formValidation = this.getFormValidationStatus(this.formEntity);
    // return this.isSubmit=false;
    this.formValidation=true;
    if (!this.formEntity.valid) {
      for (const item of this.showEntryTypeInfo.fieldTabs) {
        for (const el of item.fields) {
            const info = `${this._LocalizationService.instant(
              `Cms::The{1}FieldUnderThe{0}TAB`,
              item.name,
              el.field.displayName,
            )}`;
          EntriesToFormLabelMap[el.field.name]=info
        }
      }

      this._DigniteValidatorsService.getErrorMessage({
        form:this.formEntity,
        map:EntriesToFormLabelMap,
      });

         this.isSubmit = false;
       this.cultureInput.disable();
      return;
    }
    this._EntryAdminService
      .update(this.entryInfo.id, input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        }),
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        this._LocationBackService.backTo({
          url: `/cms/admin/entries`,
          replenish: '/edit' 
        })
        this._updateListService.updateList();
      });
  }
  isSubmit: boolean | any = false;
  draftValue: string | any = '';
  /**点击提交 */
  clickSubmit(type) {
    if (this.isSubmit) return;
    this.isSubmit = true;
    this.draftValue = type;
    this.cultureInput.enable();
    this.submitclick?.nativeElement?.click();
  }

  /**返回上一页 */
  backTo() {
    this._location.back();
  }
}
