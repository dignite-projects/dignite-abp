import { ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import {  FormBuilder, FormGroup} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {  Location } from '@angular/common';
import { CreateOrUpdateEntryInputBase } from './create-or-update-entry-input-base';
import { LocalizationService } from '@abp/ng.core';
import { EntryAdminService } from '../../../proxy/admin/entries';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums/ecms-component';
import { CmsApiService } from '../../../services';
import { UpdateListService } from '../../../services/update-list.service';



@Component({
  selector: 'cms-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Entries_Create,
    },
  ]
})
export class CreateComponent implements OnInit {
  constructor(
    private toaster: ToasterService,
    public _location: Location,
    private route: ActivatedRoute,
    private _EntryAdminService: EntryAdminService,
    private _LocalizationService: LocalizationService,
    private router: Router,
  ) { }
  private fb=inject(FormBuilder)
  private _updateListService=inject(UpdateListService)

  /**表单实体 */
  newEntity: FormGroup | undefined

  /**是否是草稿 */
  draftType: boolean = false
  /**页面传值用于传递到子组件 */
  queryParams: any = ''

  /**指定id的条目信息 */
  entrySelect: any = ''

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  async ngOnInit(): Promise<void> {
    this.queryParams = this.route.snapshot.queryParams
    this.newEntity = this.fb.group(new CreateOrUpdateEntryInputBase())
    if(this.queryParams.RevisionEntryId)   await this.getEntrySelect();
  }

  /**获取条目信息 */
  getEntrySelect() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.get(this.queryParams.RevisionEntryId).subscribe(res => {
        this.entrySelect = res
        
        resolve(res)
      })
    })
  }



  /**保存 */
  save(draft) {
    this.newEntity.patchValue({
      draft: draft
    })
    let input = this.newEntity.value
    input.publishTime = new Date(new Date(input.publishTime).getTime() + (8 * 60 * 60 * 1000)).toISOString()
    input.culture=this.newEntity.get('culture').value
    if (!this.newEntity.valid) return
    this._EntryAdminService.create(input).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
        this.router.navigate([`/cms/admin/entries`]);
        this._updateListService.updateList();
      });
    })
  }

  /**触发点击按钮替身 */
  clickSubmit(type) {
    this.draftType = type
    this.submitclick.nativeElement.click()
  }
  /**返回上一页 */
  backTo() {
    this._location.back()
  }




}
