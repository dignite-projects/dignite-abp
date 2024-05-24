import { AfterContentInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup} from '@angular/forms';
import { EntryConfig } from './entry-config';
import { SiteAdminService } from '../../../proxy/admin/sites';
import { SectionAdminService } from '../../../proxy/admin/sections';
@Component({
  selector: 'cms-entry-config',
  templateUrl: './entry-config.component.html',
  styleUrls: ['./entry-config.component.scss']
})
export class EntryConfigComponent implements AfterContentInit{
  constructor(
    private fb: FormBuilder,
    private _SiteAdminService: SiteAdminService,
    private _SectionAdminService: SectionAdminService,
  ) {
  }
  /**表单控件类型 */
  _type: any
  @Input()
  public set type(v: any) {
    if (v == this._type) return

    this._type = v
  }
  /**表单实体 */
  _Entity: FormGroup | undefined
  @Input()
  public set Entity(v: FormGroup) {
    this._Entity = v;
  }
  /**选择的表单信息 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v
  }
  /**语言 */
  _culture: any
  @Input()
  public set culture(v: any) {
    this._culture = v
  }
  get formConfiguration() {
    return this._Entity.get('formConfiguration') as FormGroup
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**站点列表 */
  siteList: any[any] = []

  /**选择的站点id */
  // siteId: string = ''
  siteId: any = new FormControl('')
  /**站点下的版块 */
  SiteOfSectionList: any[any] = []

  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    this.dataLoaded(2)
  }

  async dataLoaded(val) {
    if (this._Entity && this._type) {
      await this.AfterInit()
      this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise(async (resolve, rejects) => {
      this._Entity.setControl('formConfiguration', this.fb.group(new EntryConfig()))
      await this.getsiteList()
      await this.getSiteOfSectionList()
      if (this._selected) {
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration
        })
      }
      resolve(true)
    })
  }

  /**获取站点列表 */
  getsiteList() {
    return new Promise((resolve, rejects) => {
      this._SiteAdminService.getList({}).subscribe(res => {
        this.siteList = res.items
        this.siteId = res.items[0]?.id || ''
        resolve(res.items)
      })
    })
  }
  /**获取站点下的版块 */
  getSiteOfSectionList() {
    return new Promise((resolve, rejects) => {
      this._SectionAdminService.getList({
        maxResultCount: 1000,
        siteId: this.siteId
      }).subscribe(res => {
        this.SiteOfSectionList = res.items
        this.formConfiguration.patchValue({
          'Entry.SectionId': res.items[0]?.id || ''
        })
        resolve(res.items)
      })
    })
  }

  /**切换站点 */
  async siteIdChange() {
    await this.getSiteOfSectionList()
  }
}
