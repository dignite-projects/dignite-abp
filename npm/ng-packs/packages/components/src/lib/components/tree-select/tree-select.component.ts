import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { OnChangeType, OnTouchedType } from 'ng-zorro-antd/core/types';
import { NzFormatEmitEvent, NzTreeNode } from 'ng-zorro-antd/tree';

@Component({
  selector: 'dignite-tree-select',
  templateUrl: './tree-select.component.html',
  styleUrls: ['./tree-select.component.scss'],
  providers: [
    {
      useExisting: forwardRef(() => TreeSelectComponent),
      provide: NG_VALUE_ACCESSOR,
      multi: true,
    },
  ],
})
export class TreeSelectComponent implements OnInit, ControlValueAccessor {

  constructor() {
  }
  // onChange: OnChangeType = () => { };
  // onTouched: OnTouchedType = () => { };

  // ControlValueAccessor的四项
  writeValue(value: number | string) {
    // 刷新页面的时候，这个方法是会请求的，也就是说：这里能捕获到使用该组件所绑定的formControlName的当前值并反显
    // this.nzValue = value;
  }
  registerOnChange(fn: OnChangeType) {
    // this.onChange = fn;
  }
  registerOnTouched(fn: OnTouchedType) {
    // this.onTouched = fn;
  }
  setDisabledState(disabled: boolean) {
    // this.nzDisabled = disabled;
  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
  }

  searchValue = '';

  @Input() formControlName: any
  @Input() placeholder: any = ''
  @Input() form: FormGroup | undefined
  @Input() disabled: boolean = false
  @ViewChild('dropdown') dropdown?;
  // /**选择的节点 */
  @Input() selectedNode: any;

  @Input()
  public set ngModel(val: any) {
    if (!val) this.searchValue = ''
    if (val) {
      this.searchValue =this.allArray_nodes.find(el=>el.id===val).title||''
    }
  }



  @Output() ngModelChange = new EventEmitter<any>
  @Output() change = new EventEmitter<any>
  @Output() digniteChange = new EventEmitter<any>

  nzEvent(event: NzFormatEmitEvent) {
    this.dropdown.close()
    // console.log('nzEvent',event);

    this.searchValue = event.node.title
    this.selectedNode = event.node.origin;
    if (this.formControlName) {
      let obj = {}
      obj[this.formControlName] = event.node.origin.key
      this.form.patchValue(obj)
    }
    if (this.ngModelChange) {
      this.ngModelChange.emit(event.node.origin.key)
    }
    // 
    if (this.change) this.change.emit(event.node.origin.key)
    if (this.digniteChange) this.digniteChange.emit(event.node.origin.key)

  }

  searchChange(event) {
    if (event.target.value === '') {
      if (this.formControlName) {
        let obj = {}
        obj[this.formControlName] = ''
        this.form.patchValue(obj)
      }
      if (this.ngModelChange) {
        // console.log(2222222);
        this.ngModelChange.emit('')

      }
      if (this.change) this.change.emit('');
      if (this.digniteChange) this.digniteChange.emit('');
    }
  }




  /**tree数据 */
  _nodes: any[] = []
/**  所有数组tree数据 */
  allArray_nodes: any[] = []
  @Input()
  public set nodes(val: any[]) {
    this._nodes = this.setExpanded(val);
   this.allArray_nodes= this.openArray(this._nodes,[])
  };
    /**展开所有数组 */
    openArray(array,total){
      array.map(el=>{
        total.push(el)
        if(el.children.length>0){
          this.openArray(el.children,total)
        }
      })
      return total
    }

  /**设置展开的节点 */
  setExpanded(array: any[]) {
    let value = this.form?.value[this.formControlName]
    array.forEach(el => {
      if (el.id == value) {
        this.searchValue = el.title
        this.selectedNode = el;
      }
      if (el.children.length > 0) {
        el.children = this.setExpanded(el.children)
      } else {
        el.isLeaf = true
      }
    })
    return array
  }

}
