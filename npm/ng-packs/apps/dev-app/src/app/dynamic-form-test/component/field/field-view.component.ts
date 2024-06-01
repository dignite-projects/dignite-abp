import { ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ConfigStateService, LocalizationService } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { DynamicComponent } from '../../enums';
import { CreateOrUpdateEntryInputBase } from './field-view-input-base';
import { FieldDataService } from '../../services/field-data.service';

@Component({
  selector: 'app-field-view',
  templateUrl: './field-view.component.html',
  styleUrl: './field-view.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: DynamicComponent.FieldsView,
    },
  ]
})
export class FieldViewComponent {
  constructor(
    private toaster: ToasterService,
    public _location: Location,
    private route: ActivatedRoute,
    private _LocalizationService: LocalizationService,
    private router: Router,
    private configState: ConfigStateService,
  ) { }
  private fb = inject(FormBuilder)
  private _FieldDataService = inject(FieldDataService)

  /**表单实体 */
  newEntity: FormGroup | undefined

  /**是否是草稿 */
  draftType: boolean = false
  /**页面传值用于传递到子组件 */
  queryParams: any = ''
  /**字段id */
  fieldId: any = ''
  /**字段详情 */
  fieldDetails: any = ''
  /**系统语言 */
  systemculture:any=''
  /**指定id的条目信息 */
  entrySelect: any = ''

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  async ngOnInit(): Promise<void> {
    const _fieldId = this.route.snapshot.params.id;
    this.fieldId = _fieldId
    this.newEntity = this.fb.group(new CreateOrUpdateEntryInputBase())
    //获取系统语言 */
    this.systemculture=this.configState.getDeep('localization.currentCulture.name')
    await this.getFieldId()
    if(this.fieldDetails.extraProperties){
      this.newEntity.patchValue({
        extraProperties:this.fieldDetails.extraProperties
      })
    }else{
    }
  }
  /**获取指定字段的信息 */
  getFieldId() {
    return new Promise((resolve) => {
      this._FieldDataService.getFieldId(this.fieldId).subscribe(res => {
        this.fieldDetails = res
        resolve(res)
      })
    })
  }


  /**保存 */
  save() {
    let input = this.newEntity.value
    if (!this.newEntity.valid) return
    this._FieldDataService.setFieldExtraProperties(this.fieldId,input).subscribe(res=>{
      this._location.back()
    })
  }
  /**触发点击按钮替身 */
  clickSubmit() {
    // this.draftType = type
    this.submitclick.nativeElement.click()
  }
  /**返回上一页 */
  backTo() {
    this._location.back()
  }
}
