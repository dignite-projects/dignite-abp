import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SelectRoutingModule } from './select-routing.module';
import { ViewComponent } from './view/view.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CkedComponent } from './view/cked.component';
import { ExtensibleModule } from '@abp/ng.components/extensible';
import { PageModule } from '@abp/ng.components/page';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
import { NgbNavModule, NgbAccordionModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { CmsRoutingModule } from 'packages/cms/src/lib/cms-routing.module';

@NgModule({
  declarations: [ViewComponent, CkedComponent],
  imports: [
    CommonModule,
    SelectRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,

      CoreModule,
        // ThemeSharedModule,
        // CmsRoutingModule,
        // NgbNavModule,
        // NgbAccordionModule,
        // NgbDropdownModule,
        // PageModule,
        // NzSelectModule,
        // DynamicFormModule,
        // ExtensibleModule,
        // DragDropModule,
        // NgxDatatableModule,
        // CKEditorModule,
        // FormsModule,
  ]
})
export class SelectModule { }
