import { NgModule } from '@angular/core';

import { CkEditorRoutingModule } from './ck-editor-routing.module';
import { CoreModule } from '@abp/ng.core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CkEditorConfigComponent, CkEditorControlComponent } from './dynamic-form';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [
    CkEditorControlComponent,
    CkEditorConfigComponent,
  ],
  imports: [
    FormsModule,
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
    CkEditorRoutingModule,
    CKEditorModule
  ],
  exports: [
  ]
})
export class CkEditorModule { }
