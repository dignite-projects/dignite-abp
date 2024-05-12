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

} from './components';
import {
  MatrixConfigComponent,
  MatrixControlComponent,
  TableConfigComponent,
  TableControlComponent,
  EntryConfigComponent,
  EntryControlComponent,
  FieldControlGroup
} from "./components/dynamic-form";
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import {
  NgbDateAdapter,
  NgbDropdownModule,
  NgbNavModule,
  NgbAccordionModule,
} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiConfigModule } from '@volo/abp.commercial.ng.ui/config';
import { CMS_TOOLBAR_ACTION_CONTRIBUTORS } from './toolbar';
import { NzSelectModule } from 'ng-zorro-antd/select';
import {  DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
import { NzTableModule } from 'ng-zorro-antd/table';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { RouteReuseStrategy } from '@angular/router';
import { SimpleReuseStrategy } from './services/simple-reuse-strategy';

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
    NgbDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    NgxValidateCoreModule,
    NgbNavModule,
    NgbAccordionModule,
    DragDropModule,
    PageModule,
    CommercialUiConfigModule,
    NzSelectModule,
    NzTableModule,
    ScrollingModule,
    DynamicFormModule.forRoot({
      cmsFieldControlGroup: FieldControlGroup
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
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: RouteReuseStrategy, useClass: SimpleReuseStrategy }
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
