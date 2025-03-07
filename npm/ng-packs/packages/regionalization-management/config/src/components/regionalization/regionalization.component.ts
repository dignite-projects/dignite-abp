/* eslint-disable @angular-eslint/component-selector */
import { ConfigStateService, CoreModule, LocalizationService } from '@abp/ng.core';
import { ThemeSharedModule, ToasterService } from '@abp/ng.theme.shared';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { SimpleReuseStrategy, ValidatorsService } from '@dignite-ng/expand.core';
import { eRegionalizationManagementRouteNames } from '../../enums';
import { RegionalizationFormConfig } from './regionalization-form-config';
import { RegionalizationService } from '../../proxy/dignite/abp/regionalization-management';
import { finalize } from 'rxjs';

@Component({
  selector: 'lib-regionalization',
  standalone: true,
  imports: [CoreModule, ThemeSharedModule],
  templateUrl: './regionalization.component.html',
  styleUrl: './regionalization.component.scss',
})
export class RegionalizationComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private _LocalizationService: LocalizationService,
    private _RegionalizationService: RegionalizationService,
    private toaster: ToasterService,
    private configState: ConfigStateService,
  ) {}

  public _ValidatorsService = inject(ValidatorsService);
  
  eRegionalizationManagementRouteNames = eRegionalizationManagementRouteNames;
  /**正在提交中 */
  isSubmit: boolean|any = false;
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';
  /**系统语言 */
  languagesSystem: any[] = [];

  /**表单实体 */
  formEntity: FormGroup | any;

  get availableCultureNamesInput() {
    return this.formEntity?.get('availableCultureNames') as FormControl;
  }
  get defaultCultureNameInput() {
    return this.formEntity?.get('defaultCultureName') as FormControl;
  }

  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.formEntity = this.fb.group(new RegionalizationFormConfig());
    const languagesSystem = this.configState.getDeep('localization.languages');

    this.languagesSystem = languagesSystem;
    await this.getRegionalization();
    const availableCultureNamesValue = this.availableCultureNamesInput?.value;
    languagesSystem.map(el => {
      availableCultureNamesValue.includes(el.cultureName)
        ? (el.ischecked = true)
        : (el.ischecked = false);
      if (availableCultureNamesValue.includes(el.cultureName)) {
        el.ischecked = true;
        this.availableCultureNamesItem.push(el);
      } else {
        el.ischecked = false;
      }
    });
  }
  /**获取区域配置 */
  getRegionalization() {
    return new Promise((resolve) => {
      this._RegionalizationService.get().subscribe(
        res => {
          this.formEntity.patchValue(res);
          resolve(true);
        },
        () => {
          resolve(true);
        },
      );
    });
  }
  availableCultureNamesItem: any[] = [];
  /**设置可用语言 */
  setavailableCultureNames(item) {
    item.ischecked = !item.ischecked;
    if (item.ischecked) {
      if (!this.availableCultureNamesItem.includes(item.cultureName)) {
        this.availableCultureNamesItem.push(item);
      }
    } else {
      const index = this.availableCultureNamesItem.findIndex(
        el => el.cultureName == item.cultureName,
      );
      this.availableCultureNamesItem.splice(index, 1);
    }
    this.setAvailableCultureNamesFormValue();
  }
  setAvailableCultureNamesFormValue() {
    this.availableCultureNamesInput.patchValue(
      this.availableCultureNamesItem.map(el => el.cultureName),
    );
  }
  /**设置默认区域 */
  setDefault(item) {
    this.defaultCultureNameInput.patchValue(item.cultureName);
  }

  /**保存 */
  save() {
    const input = this.formEntity?.value;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.formEntity);
    if (
      this._ValidatorsService.isCheckForm(this.formValidation, 'RegionalizationManagementResource')
    )
      return;
    this.isSubmit = true;
    this._RegionalizationService
      .update(input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
          this.formValidation = '';
        }),
      )
      .subscribe(() => {
        SimpleReuseStrategy.deleteAllRouteCache();
        this.toaster.success(
          this._LocalizationService.instant('RegionalizationManagementResource::SavedSuccessfully'),
        );
      });
  }
}
