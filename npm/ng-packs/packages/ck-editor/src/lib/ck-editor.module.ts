import { NgModule } from '@angular/core';

import { CkEditorRoutingModule } from './ck-editor-routing.module';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CkEditorConfigComponent, CkEditorControlComponent } from './dynamic-form';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CkEditorViewComponent } from './dynamic-form/ck-editor/ck-editor-view.component';

@NgModule({
  declarations: [
    CkEditorControlComponent,
    CkEditorConfigComponent,
    CkEditorViewComponent,
  ],
  imports: [
    CoreModule,
    ThemeSharedModule,
    CkEditorRoutingModule,
    CKEditorModule,
  ],

})
export class CkEditorModule { }
