/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { Component, ElementRef, ViewChild, OnInit } from '@angular/core';
import { ECmsComponent } from '../../../enums';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {
  LocationBackService,
  UpdateListService,
  DigniteValidatorsService,
} from '@dignite-ng/expand.core';
import { FieldsDataService } from '../../../services/fields-data.service';
import { FieldsFormConfig, fieldToFormLabelMap } from './fields-form-config';

@Component({
  selector: 'cms-edit-field',
  templateUrl: './edit-field.component.html',
  styleUrl: './edit-field.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsEdit,
    },
  ],
})
export class EditFieldComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    public _service: FieldsDataService,
    private toaster: ToasterService,
    public _LocationBackService: LocationBackService,
    public _LocalizationService: LocalizationService,
    private route: ActivatedRoute,
    private _UpdateListService: UpdateListService,
    private _DigniteValidatorsService: DigniteValidatorsService,
  ) {}

  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    const { id } = this.route.snapshot.params;
    this.formEntity = this.fb.group(new FieldsFormConfig());
    await this.getFieldDetails(id);
    if (this.fieldDetail) {
      this.formEntity.patchValue({
        ...this.fieldDetail,
        groupId: this.fieldDetail.groupId || '',
      });
    }
  }

  /**获取字段详情 */
  getFieldDetails(id: string) {
    return new Promise((resolve, reject) => {
      this._service.getFieldDetail(id).subscribe(
        res => {
          this.fieldDetail = res;
          resolve(res);
        },
        err => {
          resolve('');
        },
      );
    });
  }

  /**字段详情 */
  fieldDetail: any = '';

  /*
  /**表单实体 */
  formEntity: FormGroup | undefined;

  /**表单是否触发验证 */
  formValidation = false;

  /**是否提交 */
  isSubmitted = false;

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**字段分组控件 */
  get groupIdInput() {
    return this.formEntity?.get('groupId') as FormControl;
  }
 
  // 获取错误信息
  getErrorMessage(error: any,map:any): string {
    // 获取本地化标签
    const getLocalizedLabel = (path: string) => {
      return this._LocalizationService.instant(map[path as keyof typeof map]);
    };
  
    // 格式化数组索引
    const formatArrayIndex = (item: string) => {
      const regex = /\[[0-9]+\]/g;
      const indexMatch = item.match(regex);
      const [basePath] = item.split(regex);
      return getLocalizedLabel(basePath) + (indexMatch ? indexMatch[0] : '');
    };
  
    let errorMessage = '';
    // 如果错误路径包含formConfiguration
    if (error.path.includes('formConfiguration')) {
      // 将错误路径中的formConfiguration-替换掉，并按-分割
      const pathParts = error.path.replaceAll('formConfiguration-', '').split('-');
      // 对分割后的路径进行格式化
      const formattedParts = pathParts.map((part, index) => {
        // 如果是最后一个路径且不是第一个路径
        if (index === pathParts.length - 1 && index > 0) {
          // 获取前一个路径
          const prevPart = pathParts[index - 1];
          // 将前一个路径按数组索引分割
          const [prevBasePath] = prevPart.split(/\[[0-9]+\]/);
          // 将当前路径按数组索引分割
          const [currentBasePath] = part.split(/\[[0-9]+\]/);
          // 组合前一个路径和当前路径
          const combinedKey = `${prevBasePath}-${currentBasePath}`;
          
          // 如果map中存在组合后的路径，则返回本地化标签，否则返回格式化后的数组索引
          return map[combinedKey as keyof typeof map]
            ? getLocalizedLabel(combinedKey)
            : formatArrayIndex(part);
        }
        // 否则返回格式化后的数组索引
        return formatArrayIndex(part);
      });
      console.log(formattedParts, 'formattedParts',pathParts);
      // 将格式化后的路径用-连接起来
      errorMessage = formattedParts.join('-');
    } else {
      // 否则返回本地化标签
      errorMessage = getLocalizedLabel(error.path);
    }
  
    // 添加错误信息
    errorMessage = `${errorMessage} ${this._LocalizationService.instant('AbpValidation::ThisFieldIsNotValid.')}`;
    // 显示警告信息
    this.toaster.warn(errorMessage);
    return errorMessage;
  }

  /**提交表单 */
  save() {
  
    this.formValidation = true;
    if (!this.formEntity.valid) {
      this._DigniteValidatorsService.getErrorMessage({
        form:this.formEntity,
        map:fieldToFormLabelMap
      });
      return;
    }
      console.log(this.formEntity.value, '提交表单', this.formEntity);
    // return
    if (this.isSubmitted) return;
    this.isSubmitted = true;
    const input = this.formEntity.value;
    this._service.updateField(this.fieldDetail.id, input).subscribe({
      next: () => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        // this._LocationBackService.back();
        this._LocationBackService.backTo({
          url: `/cms/admin/fields`,
          replenish: '/edit',
        });
        this._UpdateListService.updateList();
      },
      complete: () => {
        this.reset();
      },
    });
  }
  /**重置表单 */
  reset() {
    this.isSubmitted = false;
  }
}
