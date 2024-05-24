import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { Component, ElementRef, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DynamicComponent } from '../../enums';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { FieldDataService } from '../../services/field-data.service';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-field',
  templateUrl: './create-field.component.html',
  styleUrl: './create-field.component.scss',
  providers:[
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: DynamicComponent.FieldsCreate,
    },
  ]
})
export class CreateFieldComponent {

  constructor(
    // private fb: FormBuilder,
    // public _FieldAdminService: FieldAdminService,
    // private toaster: ToasterService,
    // public _location: Location,
    // public _LocalizationService: LocalizationService,
    // public _CmsApiService: CmsApiService,
    // private router: Router,
   
  ) { }
  // private _UpdateListService=inject(UpdateListService)
  private fb=inject(FormBuilder)
  private _FieldDataService=inject(FieldDataService)
  private _LocalizationService=inject(LocalizationService)
  private toaster=inject(ToasterService)
  // private _location=inject(Location)
  public _location=inject(Location)
 
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
    console.log(input,'保存表单');
    this._FieldDataService.addFieldList(input).then(res=>{
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this._location.back()
    })
  }
}
