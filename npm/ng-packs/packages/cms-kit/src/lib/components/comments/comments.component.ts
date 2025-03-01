/* eslint-disable @angular-eslint/component-selector */
import {
  ListService,
  LIST_QUERY_DEBOUNCE_TIME,
  ABP,
  LocalizationService,
  PagedResultDto,
} from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, inject, OnInit } from '@angular/core';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { Router } from '@angular/router';
import { CommentAdminService, CommentGetListInput, CommentWithAuthorDto } from '../../proxy/volo/cms-kit/admin/comments';
import { eCmsKitRouteName } from '../../enums';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';


@Component({
  selector: 'cms-comments',
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.scss',
  providers: [
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
        {
          provide: EXTENSIONS_IDENTIFIER,
          useValue: eCmsKitRouteName.Comments,
        },
  ],
})
export class CommentsComponent implements OnInit {
  public readonly list = inject(ListService);
  private router = inject(Router);
  private toaster = inject(ToasterService);
  private _confirmationService = inject(ConfirmationService);
  private _LocalizationService = inject(LocalizationService);
  private _CommentAdminService = inject(CommentAdminService);

  ColumnMode = ColumnMode;
  _eCmsKitRouteName = eCmsKitRouteName;


  /**表格数据 */
  data: PagedResultDto<CommentWithAuthorDto> = {
    items: [],
    totalCount: 0,
  };
  /** 筛选*/
  filters = {} as CommentGetListInput;

  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this._CommentAdminService.getList({
        ...query,
        ...this.filters,
      });
    const setData = (list: PagedResultDto<CommentWithAuthorDto>) => {
      list.totalCount = list.items.length;
      this.data = list;
      this.onPageChange();
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }
  onPageChange(): void {
    // 换页时滚动到顶部
    document.getElementsByClassName('lpx-scroll-container')?.item(0)?.scrollTo(0, 0);
  }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.hookToQuery();
  }

  /**获取列表 */
  getList() {
    this.list.get();
  }
  /**清除 */
  clearAll() {
    this.filters = {} as CommentGetListInput;
    this.list.get();
  }

  /**设置时间格式 */
  setDatetype(event: any, type: string) {
    if (type == 'minDate') {
      this.filters[type] = `${event.target.value}`;
    } else if (type == 'maxDate') {
      this.filters[type] = `${event.target.value}`;
    }
  }
  /**跳转详情 */
  goInfo(row) {
    this.router.navigate([`/cms/comments/${row.id}`]);
  }
  /**删除列表项 */
  deleteItem(input: any) {
    return new Promise((resolve, rejects) => {
      this._confirmationService
        .warn(
          `${input.text}`,
          this._LocalizationService.instant(`CmsKit::MessageDeletionConfirmationMessage`),
          {}
        )
        .subscribe(async (status: Confirmation.Status) => {
          if (status == 'confirm') {
            this._CommentAdminService.delete(input.id).subscribe(res => {
              this.toaster.success(this._LocalizationService.instant(`CmsKit::SuccessfullyDeleted`));
              this.getList();
              resolve(true);
            });
          }
          if (status == 'reject') return rejects(false);
        });
    });
  }
}
