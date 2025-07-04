/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries/entry-admin.service';
import { EntryStatus } from '../../../proxy/dignite/cms/entries';

@Component({
  selector: 'cms-entry-search',
  templateUrl: './entry-search.component.html',
  styleUrl: './entry-search.component.scss',
})
export class EntrySearchComponent {
  constructor() {}
  private fb = inject(FormBuilder);
  private _EntryAdminService = inject(EntryAdminService);

  /**表单实体 */
  _entity: FormGroup | any;
  @Input()
  public set entity(v: any) {
    this._entity = v;

    if (v) this.dataLoaded();
  }

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }
  // /**父级字段名称，用于为表单设置控件赋值 */
  // _selected: any;
  // @Input()
  // public set selected(v: any) {
  //   this._selected = v || [];
  // }
  /**语言 */
  _culture: any;
  @Input()
  public set culture(v: any) {
    if (v) {
      this._culture = v;
      // this.dataLoaded()
    }
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  extraProperties: FormGroup | undefined;

  /** */
  listOfOption: any[] = [];
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName && this._culture) {
      await this.AfterInit();
      await this.getEntryAssignList();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      const ValidatorsArray: any[] = [];
      // if (this._fields.required) {
      //   ValidatorsArray.push(Validators.required);
      // }
      // if (!this._fields.field.formConfiguration['Entry.Multiple']) {
      //   this.listOfSelectedValue = this._selected[0];
      // }
      const newControl = this.fb.control([], ValidatorsArray);
      const extraProperties: any = this._entity.get(this._parentFiledName) as FormGroup;
      extraProperties.setControl(this._fields.field.name, newControl);

      resolve(true);
    });
  }
  get selectInput() {
    return this._entity?.get(this._parentFiledName)?.get(this._fields.field.name) as FormControl;
  }
  listOfSelectedValue: any = '';
  ModelChange(event) {
    this.selectInput.patchValue(event?[event]:'');
  }

  /**获取对应的条目 */
  getEntryAssignList(filter = '') {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService
        .getList({
          culture: this._culture,
          sectionId: this._fields.field.formConfiguration['Entry.SectionId'],
          skipCount: 0,
          maxResultCount: 1000,
          status: EntryStatus.Published,
          // filter: filter,
        })
        .subscribe((res: any) => {
          this.listOfOption = res.items;
          resolve(true);
        });
    });
  }

  /** */
  async SelectChange(event) {
    await this.getEntryAssignList(event);
  }
}
