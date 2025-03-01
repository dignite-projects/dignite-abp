import { Component, ElementRef, Input, ViewChild, ChangeDetectorRef, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DateEditInterfaces } from '../../../enums/date-edit-interfaces';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'df-date-edit-control',
  templateUrl: './date-edit-control.component.html',
  styleUrls: ['./date-edit-control.component.scss'],
})
export class DateEditControlComponent {
  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) {}
  private _dataPipe = inject(DatePipe);
  _DateEditInterfaces = DateEditInterfaces;

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

  public get entity(): any {
    return this._entity;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity.get('extraProperties') as FormGroup;
  }
  get fieldInput() {
    return this.extraProperties.get(this._fields.field.name);
  }
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();

      this.cdr.detectChanges();
      this.submitclick.nativeElement.click();
    }
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      let ValidatorsArray = [];
      let formConfiguration = this._fields.field.formConfiguration;
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      if (formConfiguration['DateEdit.Min']) {
        ValidatorsArray.push(Validators.min(formConfiguration['DateEdit.Min']));
      }
      if (formConfiguration['DateEdit.Max']) {
        ValidatorsArray.push(Validators.max(formConfiguration['DateEdit.Max']));
      }
      let controlName = this._selected;
      if (this._fields.field.formConfiguration['DateEdit.InputMode'] === DateEditInterfaces.Date) {
        controlName = this._dataPipe.transform(this._selected, 'yyyy-MM-dd');
      }
      if (
        this._fields.field.formConfiguration['DateEdit.InputMode'] === DateEditInterfaces.DateTime
      ) {
        controlName = this._dataPipe.transform(this._selected, 'yyyy-MM-dd HH:mm:ss');
      }
      if (this._fields.field.formConfiguration['DateEdit.InputMode'] === DateEditInterfaces.Month) {
        controlName = this._dataPipe.transform(this._selected, 'yyyy-MM');
      }
      let newControl = this.fb.control(controlName, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);

      resolve(true);
    });
  }
  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.extraProperties.removeControl(this._fields.field.name);
  }
}
