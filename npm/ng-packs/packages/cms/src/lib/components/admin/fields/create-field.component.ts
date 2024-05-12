import { Component, ElementRef, ViewChild, inject } from '@angular/core';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToasterService } from '@abp/ng.theme.shared';
import { FieldAdminService } from '../../../proxy/admin/fields';
import { Location } from '@angular/common';
import { LocalizationService } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums';
import { CmsApiService } from '../../../services';
import { Router } from '@angular/router';
import { UpdateListService } from '../../../services/update-list.service';

@Component({
  selector: 'cms-create-field',
  templateUrl: './create-field.component.html',
  styleUrls: ['./create-field.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsCreate,
    },
  ]
})
export class CreateFieldComponent {
  
  constructor(
    private fb: FormBuilder,
    public _FieldAdminService: FieldAdminService,
    private toaster: ToasterService,
    public _location: Location,
    public _LocalizationService: LocalizationService,
    public _CmsApiService: CmsApiService,
    private router: Router,
   
  ) { }
  private _UpdateListService=inject(UpdateListService)
 
  /**表单实体 */
  newEntity: FormGroup | undefined 

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.newEntity= this.fb.group(new CreateOrUpdateFieldInputBase())
  }

  /**触发提交按钮 */
  submitclickBtn(){
    this.submitclick.nativeElement.click()
  }
  /**保存表单 */
  save() {
    let input = this.newEntity.value
    if (!this.newEntity.valid) return
    this._FieldAdminService.create(input).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
        this.router.navigate([`/cms/admin/fields`]);
        this._UpdateListService.updateList();
      });
    })
  }

}
