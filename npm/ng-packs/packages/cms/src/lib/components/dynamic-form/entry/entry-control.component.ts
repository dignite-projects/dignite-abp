import { Component, ElementRef, Input, ViewChild,  inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EntryAdminService } from '../../../proxy/admin/entries';
import { EntryStatus } from '../../../proxy/entries/entry-status.enum';


@Component({
  selector: 'cms-entry-control',
  templateUrl: './entry-control.component.html',
  styleUrls: ['./entry-control.component.scss']
})
export class EntryControlComponent {


  constructor(
  ) {
  }
  private fb=inject(FormBuilder)
  private _EntryAdminService=inject(EntryAdminService)

  /**表单实体 */
  _entity: FormGroup | undefined
  @Input()
  public set entity(v: any) {
    this._entity = v;
    
    if (v) this.dataLoaded()
  }

  /**字段配置列表 */
  _fields: any = ''
  @Input()
  public set fields(v: any) {
    this._fields = v;
    if (v) this.dataLoaded()
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
    if (v) this.dataLoaded()
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any
  @Input()
  public set selected(v: any) {
    this._selected = v || [];
    if (v) this.dataLoaded()
  }
  /**语言 */
_culture: any
  @Input()
  public set culture(v: any) {
    if (v) {
      this._culture = v ;
      this.dataLoaded()
    }
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  extraProperties: FormGroup | undefined

  /** */
  listOfOption: any[] = [];
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName&&this._culture) {
      await this.AfterInit()
      await this.getEntryAssignList()
      this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray = []
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required)
      }
      let newControl = this.fb.control(this._selected, ValidatorsArray)
      let extraProperties = this._entity.get(this._parentFiledName) as FormGroup
      extraProperties.setControl(this._fields.field.name, newControl)
      resolve(true)
    })
  }

  /**获取对应的条目 */
  getEntryAssignList(filter = '') {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.getList({
        culture: this._culture,
        sectionId: this._fields.field.formConfiguration['Entry.SectionId'],
        skipCount: 0,
        maxResultCount: 30,
        status: EntryStatus.Published,
        filter: filter
      }).subscribe(res => {
        this.listOfOption = res.items
        resolve(true)
      })
    })
  }

  /** */
  async SelectChange(event) {
    await this.getEntryAssignList(event)
  }
}
