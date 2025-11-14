/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'df-tree-search',
  templateUrl: './tree-search.component.html',
  styleUrls: ['./tree-search.component.scss'],
  host: { 'class': 'df-tree-search-component' }
})
export class TreeSearchComponent implements OnDestroy {
 constructor(private fb: FormBuilder) {}

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
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }

  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity?.get('extraProperties') as FormGroup;
  }
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  formConfiguration: any = '';
  AfterInit() {
    return new Promise((resolve, rejects) => {
      const ValidatorsArray: any[] = [];
      this.formConfiguration = this._fields.field.formConfiguration;
      const isMultiple = this.formConfiguration['TreeView.Multiple'];
      const selectValue: any = isMultiple ? [] : null;
      
      const treeOptions = this.formConfiguration['TreeView.Nodes'];
      if (treeOptions?.length) {
        this.nodes = this.convertTreeOptionsToNodes(treeOptions);
      }
      
      const initialValue = this._selected !== undefined ? this._selected : selectValue;
      const newControl = this.fb.control(initialValue, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);
      resolve(true);
    });
  }
  
  private convertTreeOptionsToNodes(treeOptions: any[]): any[] {
    return treeOptions.map(option => ({
      title: option.Text,
      key: option.Value,
      isLeaf: !option.Children?.length,
      children: option.Children?.length ? this.convertTreeOptionsToNodes(option.Children) : []
    }));
  }
  changeValue(event) {
    const selectvalue = this.extraProperties.get(this._fields.field.name).value;
    if (selectvalue[0] === '') {
      this.extraProperties.get(this._fields.field.name).setValue([]);
    }
  }
  ngOnDestroy(): void {
    if (this.extraProperties && this._fields?.field?.name) {
      this.extraProperties.removeControl(this._fields.field.name);
    }
  }


  nodes: any[] = [];

  onChange($event: string[]): void {
    console.log($event);
  }
}
