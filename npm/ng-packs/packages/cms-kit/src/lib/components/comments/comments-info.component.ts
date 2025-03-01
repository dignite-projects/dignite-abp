/* eslint-disable @angular-eslint/component-selector */
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentAdminService } from '../../proxy/volo/cms-kit/admin/comments';
import { FileDescriptorService } from '../../proxy/dignite/file-explorer/files';




@Component({
  selector: 'cms-comments-info',
  templateUrl: './comments-info.component.html',
  styleUrl: './comments-info.component.scss',
})
export class CommentsInfoComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private _CommentAdminService = inject(CommentAdminService);
  private _FileDescriptorService = inject(FileDescriptorService);

  /**id */
  dataId: string = '';
  /**详情 */
  dataInfo: any = '';
  /**评价图片 */
  reviewImagesList: any[] = [];

  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.dataId = this.route.snapshot.params.id;
    this.getDataInfo();
    this.getReviewImages()
  }

  /**获取详情 */
  getDataInfo() {
    return new Promise((resolve, rejects) => {
      this._CommentAdminService.get(this.dataId).subscribe((res: any) => {
        this.dataInfo = res;
        resolve(true);
      });
    });
  }
  /**获取评价图片 */
  getReviewImages() {
    var that = this;
    return new Promise((resolve, rejects) => {
      this._FileDescriptorService.getListByEntityId('CommentFiles', this.dataId).subscribe((res: any) => {
        that.reviewImagesList = res.items;
        resolve(true);
      });
    });
  }
}
