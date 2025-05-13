/* eslint-disable @typescript-eslint/no-this-alias */
/* eslint-disable @angular-eslint/component-selector */
import { CoreModule, LocalizationService } from '@abp/ng.core';
import { ThemeSharedModule, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { eTenancyDomainsManagementRouteNames } from '../../enums';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
} from '@angular/forms';
import { TenantDomainFormConfig } from './tenant-domain-form-config';
import { finalize } from 'rxjs';
import * as psl from 'psl';
import { StepName } from '../../enums/step-name';
import { TenantDomainService } from '../../proxy/dignite/abp/tenant-domain-management';
import { ClipboardModule, Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'lib-tenant-domain',
  standalone: true,
  imports: [CoreModule, ThemeSharedModule, ClipboardModule],
  templateUrl: './tenant-domain.component.html',
  styleUrl: './tenant-domain.component.scss',
})
export class TenantDomainComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private _LocalizationService: LocalizationService,
    private _TenantDomainService: TenantDomainService,
    private toaster: ToasterService,
    private clipboard: Clipboard
  ) {
  }

  eTenancyDomainsManagementRouteNames = eTenancyDomainsManagementRouteNames;
  StepName = StepName;
  /**步骤 */
  stepIndex: number|any = 0;
  /**是否提交 */
  isSubmit: boolean|any = false;
  /**表单验证 */
  formValidation: any = '';
  /**表单 */
  formEntity: FormGroup | undefined;
  /**解析的域名 */
  parsedDomain: any = '';
  /**域名信息 */
  TenantDomainInfo: any = '';

  get domainNameInput() {
    return this.formEntity?.get('domainName') as FormControl;
  }

  async ngOnInit(): Promise<void> {
    // throw new Error('Method not implemented.');
    this.formEntity = this.fb.group(new TenantDomainFormConfig());
    await this.getTenantDomain();
    this.domainNameInput.addValidators(this.DomainValidator());
    const domainName = this.TenantDomainInfo?.domainName;
    if (domainName) {
      this.domainNameInput.patchValue(domainName);
      this.stepIndex = this.StepName.finish;
    }
  }
  /**获取域名 */
  getTenantDomain() {
    return new Promise((resolve) => {
      this._TenantDomainService.get().subscribe(res => {
        this.TenantDomainInfo = res;
        resolve(res);
      },(err)=>{
        resolve(err);
      });
    });
  }

  /**域名验证 */
  DomainValidator() {
    return (control: AbstractControl): ValidationErrors | null => {
      // const parsed:any = psl.parse(control.value);
      if (!control.value) return null;
      if (!psl.isValid(control.value)) {
        return {
          repetition: this._LocalizationService.instant(
            'TenantDomainManagement::DomainNameValiderror'
          ),
        };
      }
      return null;
    };
  }
  /**拷贝 */
  copyBtn(txt: string) {
    this.clipboard.copy(txt);
    this.toaster.success(
      this._LocalizationService.instant('TenantDomainManagement::ReplicationSuccessful')
    );
  }
  /**checkCnameRecord 检查CNAME记录 */
  checkCnameRecord() {
    const that = this;
    return new Promise((resolve, reject) => {
      this._TenantDomainService.checkCnameRecord(that.domainNameInput?.value).subscribe(res => {
        if (res) {
          that.stepIndex = that.StepName.finish;
          that.toaster.success(
            that._LocalizationService.instant(
              'TenantDomainManagement::DomainNameValidSuccessful'
            )
          );
          resolve(res);
        } else {
          that.toaster.error(
            that._LocalizationService.instant(
              'TenantDomainManagement::DomainNameValidFail'
            )
          );
          reject(res);
        }
      });
    });
  }

  /**表单提交 */
  save() {
    if (this.isSubmit) return;
    this.isSubmit = true;
    this._TenantDomainService
      .connect({
        domainName: this.domainNameInput.value,
      })
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        })
      )
      .subscribe(
        () => {
          this.toaster.success(
            this._LocalizationService.instant('TenantDomainManagement::SaveSuccessful')
          );
        },
        () => {
          this.toaster.error(
            this._LocalizationService.instant('TenantDomainManagement::SaveFailure')
          );
        }
      );
  }
  /**返回上一步 */
  backLastStep(index: number) {
    this.stepIndex = index;
  }

  /**下一步 */
  nextStep(index: number) {
    if (!this.formEntity.valid) return;
    this.parsedDomain = psl.parse(this.domainNameInput.value);
    this.stepIndex = index;
  }
  /**编辑域名 */
  editbtn(index) {
    this.stepIndex = index;
  }
}
