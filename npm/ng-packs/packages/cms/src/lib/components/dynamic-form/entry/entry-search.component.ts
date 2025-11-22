/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries/entry-admin.service';
import { EntryStatus } from '../../../proxy/dignite/cms/entries';
import { SectionAdminService } from '../../../proxy/dignite/cms/admin/sections';
import { SectionType } from '../../../proxy/dignite/cms/sections';

@Component({
  selector: 'cms-entry-search',
  templateUrl: './entry-search.component.html',
  styleUrl: './entry-search.component.scss',
})
export class EntrySearchComponent {
  constructor() {}
  private fb = inject(FormBuilder);
  private _EntryAdminService = inject(EntryAdminService);
  private _SectionAdminService = inject(SectionAdminService);

  SectionType = SectionType;

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

  /**板块信息 */
  sectionInfo: any = null;

  /** */
  listOfOption: any[] = [];
  
  /**树形节点 */
  treeNodes: any[] = [];
  treeSelectValue: any;
  selectValue: any;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName && this._culture) {
      await this.AfterInit();
      await this.getSectionById();
      await this.getEntryAssignList();
      this.cdr.detectChanges();
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
  onSelectChange(value: any) {
    const arrayValue = this._fields.field.formConfiguration['Entry.Multiple'] ? value : (value ? [value] : []);
    this.selectInput?.setValue(arrayValue, { emitEvent: false });
  }

  onTreeSelectChange(value: any) {
    const arrayValue = this._fields.field.formConfiguration['Entry.Multiple'] ? value : (value ? [value] : []);
    this.selectInput?.setValue(arrayValue, { emitEvent: false });
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
          if (this.isStructureType()) {
            this.treeNodes = this.convertToTreeNodes(res.items);
          }
          resolve(true);
        });
    });
  }
  
  /**判断是否为结构类型 */
  isStructureType(): boolean {
    return this.sectionInfo?.type === 1 || this.sectionInfo?.type === SectionType.Structure;
  }
  
  /**转换为树形节点 */
  private convertToTreeNodes(items: any[]): any[] {
    const map = new Map();
    const roots: any[] = [];
    
    items.forEach(item => {
      map.set(item.id, { title: item.slug, key: item.id, isLeaf: true, children: [] });
    });
    
    items.forEach(item => {
      const node = map.get(item.id);
      if (item.parentId) {
        const parent = map.get(item.parentId);
        if (parent) {
          parent.children.push(node);
          parent.isLeaf = false;
        } else {
          roots.push(node);
        }
      } else {
        roots.push(node);
      }
    });
    
    return roots;
  }

  /**获取对应的板块 */
  getSectionById() {
    return new Promise((resolve, rejects) => {
      this._SectionAdminService.get(this._fields.field.formConfiguration['Entry.SectionId']).subscribe((res: any) => {
        this.sectionInfo = res;
        resolve(true);
      });
    });
  }

  /** */
  async SelectChange(event) {
    await this.getEntryAssignList(event);
  }
}
