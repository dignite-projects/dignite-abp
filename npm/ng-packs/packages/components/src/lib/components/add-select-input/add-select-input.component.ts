import { Component, EventEmitter, Input, OnInit, Output, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgbNavConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dignite-add-select-input',
  templateUrl: './add-select-input.component.html',
  styleUrls: ['./add-select-input.component.scss'],
  providers: [
    {
      useExisting: forwardRef(() => AddSelectInputComponent),
      provide: NG_VALUE_ACCESSOR,
      multi: true, 
    },
  ],
})
export class AddSelectInputComponent implements OnInit, ControlValueAccessor {
  /**
   * 支持model√
   * formControlName√
   * 禁用disabled，
 
   * placeholder
   * 自定义class
   * 回调
   * 传入数据
   */
  constructor(config: NgbNavConfig) {
    // customize default values of navs used by this component tree
    config.destroyOnHide = false;
    config.roles = false;
  }
  // onChange: OnChangeType = () => { };
  // onTouched: OnTouchedType = () => { };
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }
  ngOnInit(): void {
  }

  // @Input()nodes:any[]
  _nodes: any[] = []
  _nodes_view: any[] = []
  @Input()
  public set nodes(v: any[]) {
    this._nodes = v;
    this._nodes_view = v;

  }


  @Input() form: FormGroup | undefined
  @Input() disabled: boolean = false
  @Input() placeholder: any = ''
  /**选择的数值改变回调 */
  @Output() digniteChange = new EventEmitter<any>()
  @Input()
  public set ngModel(val: any) {
  }
  _formControlName: any;
  @Input()
  public set formControlName(v: any) {
    this._formControlName = v;
    console.log(v,'_formControlName_formControlName_formControlName');
    
  }
  /**选择的数据列表 */
  // selectList: any[] = []
  @Input()selectList:any[]=[]

  @Output() ngModelChange = new EventEmitter<any>

  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    let selected =[]
    if(this._formControlName) selected = this.form.get(this._formControlName).value
    if(this.selectList.length>0) selected=this.selectList
    let filter= this._nodes.filter(el=>selected.includes(el.id))
    this.selectList=filter
    // console.log(this._nodes,filter,'ngAfterContentInit',selected,this.form.value);
  }

  /**选择的单个数据 */
  selectValueName: any = ''
  selectValue: any = ''
  /**选择单个数据 */
  SelectItem(item) {
    if(!item) return
    this.selectValue = item
    this.selectValueName = item?.title || ''
    this.setSelectview()
  }
  /**输入框筛选 */
  selectInput(event) {
    this.selectValueName = event.target.value
    this.setSelectview()
  }
  /**设置下拉框数据 */
  setSelectview() {
    let viewdata = this._nodes.filter(el => el.title.includes(this.selectValueName))
    this._nodes_view = viewdata
    
    
  }
  

  /**添加到选择的数据列表 */
  AddSelectList() {
    if(!this.selectValue) return
    let selectfind = this.selectList.find(el => el.id == this.selectValue.id)
    if (!selectfind) {
      this.selectList.push(this.selectValue)
    }
    this.setFromData()
    this.clearInput()

  }
  /**删除选择的指定数据列表 */
  deleteSelectList(index) {
    this.selectList.splice(index, 1)
    this.setFromData()
  }
  /**清空输入框 */
  clearInput() {
    this.selectValue = ''
    this.selectValueName = ''
    this.setSelectview()
  }
  /**设置表单数据 */
  setFromData() {
    let selectListId = this.selectList.map(el => el.id)
    if (this.digniteChange) this.digniteChange.emit(selectListId);

    if (this._formControlName) {
      let obj = {}
      obj[this._formControlName] = selectListId
      this.form.patchValue(obj)
    }
    if (this.ngModelChange) this.ngModelChange.emit(selectListId);

  }
}
