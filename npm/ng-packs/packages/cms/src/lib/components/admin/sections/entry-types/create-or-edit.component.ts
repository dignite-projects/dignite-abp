/* eslint-disable no-prototype-builtins */
/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {
  CreateOrEditEntryTypeInputBase,
  fieldTabsBase,
  fieldsBase,
} from './create-or-edit.-entry-type-input-base';
import { ECmsComponent } from '../../../../enums';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import {
  FieldAdminService,
  FieldGroupAdminService,
} from '../../../../proxy/dignite/cms/admin/fields';
import { EntryTypeAdminService } from '../../../../proxy/dignite/cms/admin/sections';
import { FormControlsService } from '../../../../services/form-controls.service';

import { ToPinyinService, LocationBackService, UpdateListService } from '@dignite-ng/expand.core';

@Component({
  selector: 'cms-create-or-edit',
  templateUrl: './create-or-edit.component.html',
  styleUrls: ['./create-or-edit.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.SectionsCreateOrEdit,
    },
  ],
})
export class CreateOrEditComponent implements OnInit {
  constructor(
    private toaster: ToasterService,
    public _LocationBackService: LocationBackService,
    private route: ActivatedRoute,
    public _FieldGroupAdminService: FieldGroupAdminService,
    public _FieldAdminService: FieldAdminService,
    public _EntryTypeAdminService: EntryTypeAdminService,
    public _LocalizationService: LocalizationService,
    public _FormControlsService: FormControlsService,
     private toPinyinService:ToPinyinService
  ) {}
  private fb = inject(FormBuilder);
  private _UpdateListService = inject(UpdateListService);
  /**表单实体 */
  newEntity: FormGroup | any;
  /**版块id */
  sectionId: string | any = '';
  /**条目类型id */
  entryTypesId: string | any = '';
  /**条目类型详情 */
  entryTypesSelect: any;

  /**是否全选 */
  isAllSelect={
    isRequired:false,
    isShowInList:false,
    isEnableSearch:false
  };

  get fieldTabs() {
    return this.newEntity?.get('fieldTabs') as FormArray;
  }

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click();
  }

  /**需要查询的动态表单类型 */
  enableSearchTypeList: any[] = [];
  /**不需要展示的动态表单类型 */
  disableshowinTypeList: any[] = [];
  async ngOnInit() {
    const sectionId = this.route.snapshot.params.sectionsId;
    //条目类型id
    this.entryTypesId = this.route.snapshot.params.entryTypesId || '';
    //板块id
    this.sectionId = sectionId;
    this.newEntity = this.fb.group(new CreateOrEditEntryTypeInputBase());
   
    //获取条目类型详情
    if(this.entryTypesId){
      await this.getEntryTypes();
    }
    //获取字段分组
    await this.getFieldGroup();
    //处理数据
    this.processData();
  }
  /**处理数据 */
  processData() {
    return new Promise((resolve, rejects) => {
      const fieldList: any = structuredClone(this.fieldList);
      const res = this.entryTypesSelect;
      if (!res) {
        this.addControlToFieldTabs();
      }else{
        res.fieldTabs.forEach(el => {
          this.addControlToFieldTabs();
          el.fields.forEach((eld: any) => {
            eld.id = eld.fieldId;
            eld.groupId = fieldList.find(elfd => elfd.id == eld.fieldId).groupId;
            this.formRightGroup.push(eld);
            const fieldindex = fieldList.findIndex(elfl => elfl.id == eld.fieldId);
            fieldList.splice(fieldindex, 1);
          });
        });
       
        this.fieldGroupList.forEach((el: any, index) => {
          el.fields = fieldList.filter(els => els.groupId === el.id);
        });
        this.newEntity?.patchValue(res);
        this.navActive = 0;
        this.resultSource = res.fieldTabs;

       this.setAllSelectStauts();

        console.log(this.isAllSelect,'this.isAllSelect',res.fieldTabs,this.navActive);
      }
      resolve(true);
    });
  }
  /**设置全选状态 */
  setAllSelectStauts(){
    this.isAllSelect.isRequired= this.resultSource[this.navActive].fields.every(eld=>eld.required);
        this.isAllSelect.isShowInList= this.resultSource[this.navActive].fields.every(eld=>eld.showInList);
        this.isAllSelect.isEnableSearch= this.resultSource[this.navActive].fields.every(eld=>eld.enableSearch);
  }

  /**给fieldTabs添加新控件 */
  addControlToFieldTabs(nameValue = '') {
    const newFormGroup = this.fb.group(new fieldTabsBase());
    newFormGroup
      .get('name')
      ?.patchValue(
        this.fieldTabs.length === 0
          ? this._LocalizationService.instant(`Cms::FieldTab`)
          : nameValue,
      );
    this.fieldTabs.push(newFormGroup);
    this.resultSource.push(newFormGroup.value);
    this.navActive = this.resultSource.length - 1;
  }

  /**获取字段分组 */
  getFieldGroup() {
    return new Promise((resolve, rejects) => {
      this._FieldGroupAdminService.getList({}).subscribe(async res => {
        const fieldList: any = await this.getFieldList();
        const fieldGroupList: any = res.items;
        fieldGroupList.unshift({
          id: null,
          name: 'UngroupedFields',
        });
        fieldGroupList.forEach((el: any, index) => {
          el.fields = fieldList.filter(els => els.groupId === el.id);
        });
        this.fieldGroupList = fieldGroupList;
        this.fieldList = structuredClone(fieldList);
        resolve(res);
      });
    });
  }
  /**获取条目类型详情 */
  getEntryTypes() {
    return new Promise((resolve, rejects) => {
      this._EntryTypeAdminService.get(this.entryTypesId).subscribe(res => {
        this.entryTypesSelect = res;
        resolve(res);
      });
    });
  }
 
  /**
   *
   * @param nameValue 获取所有字段
   */
  getFieldList() {
    return new Promise((resolve, rejects) => {
      this._FieldAdminService
        .getList({
          maxResultCount: 1000,
        })
        .subscribe((res: any) => {
          res.items.forEach(el => {
            el.required = false;
            el.showInList = false;
            el.enableSearch = false;
          });
          resolve(res.items);
        });
    });
  }

  /**
   * 拖拽 功能*/
  /**数据源 */
  /**数据源-字段分组数据-包含字段数据 fields */
  fieldGroupList: any[] = [];
  /**数据源-所有字段列表 */
  fieldList: any[] = [];
  /**数据源拖拽的分组下标 */
  DataSourceGroupIndex: number | any;
  /**数据源拖拽的字段下标 */
  DataSourceFieldIndex: number | any;
  /**目标源 结果*/
  resultSource: any[] = [];

  /**从数据源拖拽的元素 */
  fromDataSourceDragEl: any;
  /**从目标源拖拽的元素 */
  fromResultSourceDragEl: any;

  /**来自数据源的集合，用于从目标源拖回数据源时的判断，与取值 */
  formRightGroup: any[] = [];
  /**从数据源开始拖拽 */
  fromDataSourceDragStart(element, fieldIndex, groupIndex) {
    this.fromDataSourceDragEl = element;
    this.DataSourceFieldIndex = fieldIndex;
    this.DataSourceGroupIndex = groupIndex;
    this.fromResultSourceDragEl = undefined;
  }
  /**从目标源开始拖拽 */
  fromResultSourceDragStart(element) {
    this.fromResultSourceDragEl = element;
    this.fromDataSourceDragEl = undefined;
    this.DataSourceFieldIndex = undefined;
    this.DataSourceGroupIndex = undefined;
  }
  /**拖拽到数据源时触发 */
  dragToDataSourceDropped() {
    //从数据源拖拽到数据源-排序
    if (this.fromDataSourceDragEl) {
      /* empty */
    }
    const _fromResultSourceDragEl = this.fromResultSourceDragEl;
    const formRightGroup = structuredClone(this.formRightGroup);
    const fieldList = structuredClone(this.fieldList);

    if (_fromResultSourceDragEl) {
      //移动
      //从目标源拖拽到数据源
      // 拖拽目标源的下标
      const dragResultSourceIndex = this.resultSource[this.navActive].fields.findIndex(
        el => el.id == _fromResultSourceDragEl.id,
      );
      //删除目标源中的数据
      this.resultSource[this.navActive].fields.splice(dragResultSourceIndex, 1);
      formRightGroup.splice(
        formRightGroup.findIndex(el => el.id == _fromResultSourceDragEl.id),
        1,
      );
      this.fieldGroupList.forEach(el => {
        if (el.id == _fromResultSourceDragEl.groupId) {
          const elFieldsAll = fieldList.filter(els => els.groupId === el.id);
          el.fields = elFieldsAll.filter(
            item => !formRightGroup.some(itemB => item.id === itemB.id),
          );
        }
      });
      this.formRightGroup = formRightGroup;
    }

    this.setfieldTabsFrom();
    this.setAllSelectStauts();
  }
  /**拖拽到目标源时触发
   *
   */
  dragToResultSourceDropped(fieldTabstem: any, fieldTabsIndex) {
    const _fromDataSourceDragEl = this.fromDataSourceDragEl;
    //从数据源拖拽到目标源
    if (_fromDataSourceDragEl) {
      this.fieldGroupList[this.DataSourceGroupIndex || 0].fields.splice(
        this.DataSourceFieldIndex,
        1,
      );
      this.resultSource[fieldTabsIndex].fields.push(_fromDataSourceDragEl);
      this.formRightGroup.push(_fromDataSourceDragEl);
    }

    this.setfieldTabsFrom();
    this.setAllSelectStauts();
  }
  /** 从目标源拖拽到目标源*/
  dragToResultSourceItemDropped(fieldsIndex) {
    const _fromResultSourceDragEl = this.fromResultSourceDragEl;
    if (_fromResultSourceDragEl) {
      // 拖拽目标源的下标
      const dragResultSourceIndex = this.resultSource[this.navActive].fields.findIndex(
        el => el.id == _fromResultSourceDragEl.id,
      );
      //删除目标源中的数据
      this.resultSource[this.navActive].fields.splice(dragResultSourceIndex, 1);
      this.resultSource[this.navActive].fields.splice(fieldsIndex, 0, _fromResultSourceDragEl);
    }
    this.setfieldTabsFrom();
    this.setAllSelectStauts();
  }
  /**设置formA表单 */
  setfieldTabsFrom() {
    const setArray: any[] = [];
    this.resultSource.forEach(el => {
      const fieldsArray: any[] = [];
      el.fields.forEach(item => {
        fieldsArray.push({
          fieldId: item.id || item.fieldId,
          displayName: item.displayName,
          required: item?.required,
          showInList: item?.showInList,
          enableSearch: item?.enableSearch,
        });
      });
      setArray.push({
        name: el.name,
        fields: fieldsArray,
      });
    });
    this.newEntity?.patchValue({
      fieldTabs: setArray,
    });
  }
  private _ValidatorsService = inject(ValidatorsService);
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';
  isSubmit: boolean | any = false;
  /**保存表单 */
  save() {
    if (this.isSubmit) return;
    this.isSubmit = true;
    const input = this.newEntity?.value;

    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.newEntity);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms'))
      return (this.isSubmit = false);

    if (this.entryTypesSelect) {
      this._EntryTypeAdminService
        .update(this.entryTypesSelect.id, input)
        .pipe(
          finalize(() => {
            this.isSubmit = false;
          }),
        )
        .subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`Cms::SavedSuccessfully`));
          // this._location.back();
             this._LocationBackService.backTo({
          url: `/cms/admin/fields`,
          replenish: '/edit' 
        })
          
          this._UpdateListService.updateList();
        });
      return;
    }
    input.sectionId = this.sectionId;
    this._EntryTypeAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        }),
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`Cms::SavedSuccessfully`));
        // this._location.back();
           this._LocationBackService.backTo({
          url: `/cms/admin/sections`,
          replenish: '/create' 
        })
        this._UpdateListService.updateList();
      });
  }
  /**编辑Tabs表单 */
  editFieldTabsFrom: FormGroup;
  /**模态框状态 */
  visibleTabsOpen: boolean | any = false;
  /**是否是忙碌状态 */
  modalBusy: boolean | any = false;
  /**正在编辑的tabs */
  editFieldTabsSelect: any;
  /**正在选中的nav */
  navActive: any = 0;

  /**表单控件模板-动态赋值表单控件 */
  @ViewChild('editFieldTabsModalSubmitBtn', { static: false })
  editFieldTabsModalSubmitBtn: ElementRef;

  /**模态框状态改变回调 */
  VisibleTabsChange(event) {
    if (!event) {
      this.editFieldTabsSelect = '';
      this.formValidation = '';
      return;
    }
  }
  /**新建增加FieldTabs */
  addFieldTabs() {
    this.editFieldTabsSelect = '';
    this.editFieldTabsFrom = this.fb.group(new fieldTabsBase());
    this.visibleTabsOpen = true;
  }
  /**正在编辑的tab下标 */
  newEditFieldTabsIndex: number | any;
  /**编辑FieldTabs */
  editFieldTabs(item, i) {
    this.editFieldTabsFrom = this.fb.group(new fieldTabsBase());
    this.editFieldTabsFrom.patchValue({
      name: item.name,
    });
    this.editFieldTabsSelect = item;
    this.visibleTabsOpen = true;
    this.newEditFieldTabsIndex = i;
  }

  /**保存编辑tabs表单 */
  editFieldTabsSave() {
    if (!this.editFieldTabsFrom.value.name) {
      return;
    }
    //编辑
    if (this.editFieldTabsSelect) {
      this.resultSource[this.newEditFieldTabsIndex].name = this.editFieldTabsFrom.value.name;
      this.navActive = this.newEditFieldTabsIndex;
    } else {
      //新建
      this.addControlToFieldTabs(this.editFieldTabsFrom.value.name);
    }
    this.visibleTabsOpen = false;
    this.VisibleTabsChange(false);
    this.setfieldTabsFrom();
  }
  /**删除某个tabs表单 */
  deleteFieldTabs(index) {
    this.deleteTabFieldGroupList(this.resultSource[index].fields);
    // return
    this.fieldTabs.removeAt(index);
    this.resultSource.splice(index, 1);
    if(this.navActive===index){
      this.navActive = 0;
    }
    if(this.navActive>index){
      this.navActive = this.navActive-1;
    }
  }

  /**删除tab标签后还原 fieldGroupList */
  deleteTabFieldGroupList(deleteFields) {
    //原数据
    const fieldList=structuredClone(this.fieldList);
    //右侧手风琴数据
    const formRightGroup=structuredClone(this.formRightGroup);

    // 获取要删除的原数据
    const deleteFieldList = fieldList.filter(el=>deleteFields.map(item => item.id).includes(el.id)) ;
    deleteFieldList.forEach(item=>{
      const index=formRightGroup.findIndex(el=>el.id===item.id);
      formRightGroup.splice(
        index,
        1,
      );
    })
    
    const deleteFieldListGroupIds=deleteFieldList.map(item=>item.groupId);
    this.fieldGroupList.forEach(el => {
      if ( deleteFieldListGroupIds.includes(el.id)) {
        const elFieldsAll = fieldList.filter(els => els.groupId === el.id);
        el.fields = elFieldsAll.filter(
          item => !formRightGroup.some(itemB => item.id === itemB.id),
        );
      }
    });
    this.formRightGroup=formRightGroup;

    this.setfieldTabsFrom();
  }

  /**编辑字段模态框状态 */
  visibleEditFieldOpen: boolean | any = false;
  /**编辑字段模态框表单 */
  editFieldFrom: FormGroup;
  /**表单控件模板-动态赋值表单控件-编辑字段 */
  @ViewChild('editFieldModalSubmitBtn', { static: false }) editFieldModalSubmitBtn: ElementRef;
  /**正在编辑的字段下标 */
  EditFieldIndex: number | any;
  /**编辑字段模态框状态状态改变回调 */
  VisibleEditFieldChange(event) {
    if (!event) {
      this.EditFieldIndex = undefined;
      return;
    }
  }
  isShowInList: boolean | any = true;
  isEnableSearch: boolean | any = true;
  /**打开编辑字段模态框 */
  EditFieldModalOpen(items, elIndex) {
    this.visibleEditFieldOpen = true;
    this.EditFieldIndex = elIndex;
    this.editFieldFrom = this.fb.group(new fieldsBase());
    const fieldsOptions = this.fieldTabs.value[this.navActive].fields[elIndex];
    // this.isShowInList = !this.disableshowinTypeList.includes(fieldsOptions.field.formControlName);
    // this.isEnableSearch = this.enableSearchTypeList.includes(fieldsOptions.field.formControlName);
    this.editFieldFrom.patchValue(fieldsOptions);
    // this.disableshowinTypeList
  }
  /**保存编辑字段 */
  editFieldSave() {
    const input = this.editFieldFrom.value;
    this.resultSource[this.navActive].fields[this.EditFieldIndex].displayName = input.displayName;
    this.visibleEditFieldOpen = false;
    this.setfieldTabsFrom();
  }

  /**点击“字段布局”复选框事件 */
  checkboxChange(event,input) {
    const {checked,name}=event.target;
    const {index}=input;
    this.resultSource[this.navActive].fields[index][name]=checked;
    this.setfieldTabsFrom();
    this.setAllSelectStauts();
    // console.log(event.target,'点击“字段布局”复选框事件 ',event,input,'resultSource',this.resultSource,'newEntity',this.newEntity);
  }

  /**全选状态改变 */
  allSelectChange(event,name){
    const {checked}=event.target;
    this.resultSource[this.navActive].fields.forEach(el=>{
      el[name]=checked;
    })
    console.log(this.resultSource[this.navActive].fields,'this.resultSource[this.navActive].fields',name,checked);
    this.setfieldTabsFrom();
    this.setAllSelectStauts();
  }




  /**name表单控件 */
  get nameInput() {
    return this.newEntity.get('name');
  }
  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    const value = event.target.value;
    const pinyin = this.toPinyinService.get(value);
    const nameInput: any = this.nameInput;
    if (nameInput.value) return;
    nameInput.patchValue(pinyin);
  }
}
