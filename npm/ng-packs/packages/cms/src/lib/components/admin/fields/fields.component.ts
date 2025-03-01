
import { EXTENSIONS_IDENTIFIER } from "@abp/ng.components/extensible";
import { ListService, LIST_QUERY_DEBOUNCE_TIME, LocalizationService, PagedResultDto, ABP } from "@abp/ng.core";
import { ToasterService, ConfirmationService, Confirmation } from "@abp/ng.theme.shared";
import { Component, OnInit, inject } from "@angular/core";
import { Router } from "@angular/router";
import { ColumnMode } from "@swimlane/ngx-datatable";
import { finalize } from "rxjs";
import { ECmsComponent } from "../../../enums";
import { UpdateListService } from "@dignite-ng/expand.core";
import { FieldAdminService, FieldDto, GetFieldsInput } from "../../../proxy/dignite/cms/admin/fields";

@Component({
  selector: 'cms-fields',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.scss'],
  providers: [
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Fields,
    },
  ]
})
export class FieldsComponent implements OnInit {

  constructor(
    public readonly list: ListService,
    private _FieldAdminService: FieldAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private _LocalizationService: LocalizationService,
    private router: Router,
  ) {
    
  }
  private _UpdateListService=inject(UpdateListService)
  /**表格单元格布局类型 */
  ColumnMode = ColumnMode;

  /**表格数据 */
  data: PagedResultDto<FieldDto> = {
    items: [],
    totalCount: 0,
  };

  /**过滤器 */
  filters = {} as GetFieldsInput;

  ngOnInit(): void {
    this.hookToQuery()
    this._UpdateListService.updateListEvent.subscribe(() => {
      this.list.get()
    });
  }
  getData(){
    this.list.get()
  }


  /**字段分组选择回调 */
  fieldGroupChange(event) {
    this.filters.groupId = event
    this.list.page=0
    this.list.get()
  }



  /**使用abp的list获取表格的字段数据列表 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) => this._FieldAdminService.getList({
      ...query,
      ...this.filters,
    });
    const setData = (list: PagedResultDto<FieldDto>) => (this.data = list);
    this.list.hookToQuery(getData).subscribe(setData);
  }

  /**新建字段按钮 */
  toFieldsCreateBtn() {
    this.router.navigate(['/cms/admin/fields/create'],
      {
        queryParams: {}
      }
    )
  }

  /**删除字段 */
  deletefield(row: any) {
    this.confirmation.warn(
      row.displayName,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._FieldAdminService.delete(row.id).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
          this.list.get()
        })
      }
    });
  }
}
