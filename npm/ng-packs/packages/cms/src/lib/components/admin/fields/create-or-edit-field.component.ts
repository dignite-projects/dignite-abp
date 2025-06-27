/* eslint-disable @angular-eslint/component-selector */
import { AfterContentInit, Component, ElementRef, Input,  ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { ToPinyinService } from '@dignite-ng/expand.core';
import { Observable } from 'rxjs';
import { LocalizationService } from '@abp/ng.core';

@Component({
  selector: 'cms-create-or-edit-field',
  templateUrl: './create-or-edit-field.component.html',
  styleUrl: './create-or-edit-field.component.scss',
})
export class CreateOrEditFieldComponent implements AfterContentInit {
  constructor(private _ToPinyinService: ToPinyinService,private _LocalizationService:LocalizationService) {}

  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }

  @Input() public service: any;

  formEntity: FormGroup | undefined;
  @Input()
  public set form(v: FormGroup | undefined) {
    this.formEntity = v;
  }



  async ngAfterContentInit(): Promise<void> {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    this.getfieldGroupsList = await this.service.getfieldGroups();
    this.fromControlList = await this.service.getControlsfieldTypes();
    this.nameInput.addAsyncValidators([this.repetitionAsyncValidator()])
    if(!this.formControlNameInput.value&&this.fromControlList.length>0){
      this.formControlNameInput.patchValue(this.fromControlList[0].name)
    }
    this.submitclick.nativeElement.click()
  }

  /**字段分组列表 */
  getfieldGroupsList: any[] = [];
  /**表单控件列表 */
  fromControlList: any[] = [];

  get nameInput() {
    return this.formEntity?.get('name') as FormControl;
  }
  get formControlNameInput() {
    return this.formEntity?.get('formControlName') as FormControl;
  }

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    const value = event.target.value;
    const pinyin = this._ToPinyinService.get(value);
    const nameInput = this.nameInput;
    if (nameInput.value) return;
    nameInput.patchValue(pinyin);
  }

  /**异步验证，验证别名 */
  repetitionAsyncValidator() {
    return (
      ctrl: AbstractControl,
    ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        if (ctrl.value == this._selected?.name || !ctrl.value) {
          resolve(null);
          return;
        }
        this.service.nameExists(ctrl.value).subscribe(res => {
          if (res) {
            resolve({
              repetition: this._LocalizationService.instant(
                `Cms::FieldName{0}AlreadyExist`,
                ctrl.value,
              ),
            });
          } else {
            resolve(null);
          }
        });
      });
    };
  }
}
