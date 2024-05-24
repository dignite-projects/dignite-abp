import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CmsRoutingModule } from './cms-routing.module';
import {
  CreateComponent,
  CreateFieldComponent,
  CreateOrEditComponent,
  CreateOrEditEntriesComponent,
  CreateOrEditFieldComponent,
  EditComponent,
  EditFieldComponent,
  EntriesComponent,
  FieldGroupComponent,
  FieldsComponent,
  SectionsComponent,
  SitesComponent,
  MatrixConfigComponent,
  MatrixControlComponent,
  TableConfigComponent,
  TableControlComponent,
  EntryConfigComponent,
  EntryControlComponent,
  FieldControlGroup
} from './components';
import {
  NgbDropdownModule,
  NgbNavModule,
  NgbAccordionModule,
} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { PageModule } from '@abp/ng.components/page';
import { CMS_TOOLBAR_ACTION_CONTRIBUTORS } from './toolbar';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
import { fielFieldControlGroup } from '@dignite-ng/expand.file-explorer';

@NgModule({
  declarations: [
    EntriesComponent,
    FieldsComponent,
    SitesComponent,
    SectionsComponent,
    FieldGroupComponent,
    CreateFieldComponent,
    EditFieldComponent,
    CreateOrEditFieldComponent,
    CreateOrEditComponent,
    CreateComponent,
    EditComponent,
    CreateOrEditEntriesComponent,
    TableConfigComponent,
    TableControlComponent,
    MatrixConfigComponent,
    MatrixControlComponent,
    EntryConfigComponent,
    EntryControlComponent,
  ],
  imports: [
    CoreModule,
    ThemeSharedModule,
    CmsRoutingModule,
    FormsModule,
    NgbNavModule,
    NgbAccordionModule,
    NgbDropdownModule,
    PageModule,
    NzSelectModule,
    DynamicFormModule.forRoot({
      cmsFieldControlGroup: [...FieldControlGroup,...fielFieldControlGroup]
    }),
  ],
  exports: [
    TableConfigComponent,
    TableControlComponent,
    MatrixConfigComponent,
    MatrixControlComponent,
    EntryConfigComponent,
    EntryControlComponent,
  ],
  providers: [
  ],

})
export class CmsModule {
  static forChild(options: any = {}): ModuleWithProviders<CmsModule> {
    return {
      ngModule: CmsModule,
      providers: [
        {
          provide: CMS_TOOLBAR_ACTION_CONTRIBUTORS,
          useValue: options.toolbarActionContributors,
        },
      ],
    };
  }
  static forLazy(options: any = {}): NgModuleFactory<CmsModule> {
    return new LazyModuleFactory(CmsModule.forChild(options));
  }
}
