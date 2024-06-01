import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DynamicFormTestRoutingModule } from './dynamic-form-test-routing.module';
import { PageModule } from '@abp/ng.components/page';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import {
  CreateOrEditFieldComponent,
  CreateFieldComponent,
  FieldsComponent,
  EditFieldComponent,
  FieldViewComponent
} from './component';
import { DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CkEditorDemoComponent } from './component/ck-editor-demo/ck-editor-demo.component';
import { ckEditorFieldControlGroup, CkEditorModule } from '@dignite-ng/expand.ck-editor';


@NgModule({
  declarations: [
    CreateFieldComponent,
    CreateOrEditFieldComponent,
    FieldsComponent,
    EditFieldComponent,
    FieldViewComponent,
    CkEditorDemoComponent
  ],
  imports: [
    CommonModule,
    DynamicFormTestRoutingModule,
    CoreModule,
    ThemeSharedModule,
    PageModule,
    FormsModule,
    NgbDropdownModule,
    DynamicFormModule,
    DynamicFormModule.forRoot({
      cmsFieldControlGroup: [...ckEditorFieldControlGroup]
    }),
    CkEditorModule
  ]
})
export class DynamicFormTestModule { }
