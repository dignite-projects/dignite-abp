import { ModuleWithProviders, NgModule, NgModuleFactory } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CmsKitRoutingModule } from './cms-kit-routing.module';
import { MenuItemComponent } from './components/menu-item/menu-item.component';
import {
  Entity_Action_Contributors,
  Entity_Props_Contributors,
  Toolbar_Action_Contributors,
} from './resolvers/extensions-props-action-token.resolver';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { PageModule } from '@abp/ng.components/page';
import { ExtensibleModule } from '@abp/ng.components/extensible';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommentsComponent } from './components/comments/comments.component';
import { CommentsInfoComponent } from './components/comments/comments-info.component';
import { CkeditorLayoutComponent } from './expand/ckeditor-layout.component';
import { ImageLayoutComponent } from './expand/image-layout.component';
import { TextStyleLayoutComponent } from './expand/text-style-layout.component';
import {  GetTenantImgPipe, ImagePreviewComponent, KeydownPreventDefaultDirective } from '@dignite-ng/expand.core';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { TreeModule } from '@abp/ng.components/tree';
import { CreateOrEditmenuItemModalComponent } from './components/menu-item/create-or-editmenu-item-modal.component';

@NgModule({
  declarations: [
    MenuItemComponent,
    CommentsComponent,
    CommentsInfoComponent,
    TextStyleLayoutComponent,
    ImageLayoutComponent,
    CkeditorLayoutComponent,
    CreateOrEditmenuItemModalComponent,
  ],
  imports: [
    CommonModule,
    CmsKitRoutingModule,
    CoreModule,
    ThemeSharedModule,
    PageModule,
    ExtensibleModule,
    NgbDropdownModule,
    CKEditorModule,
    TreeModule,
    ImagePreviewComponent,
    KeydownPreventDefaultDirective,
    GetTenantImgPipe,
  ],
  providers:[
    GetTenantImgPipe
  ]
})
export class CmsKitModule {
  static forChild(options: any = {}): ModuleWithProviders<CmsKitModule> {
    return {
      ngModule: CmsKitModule,
      providers: [
        // {
        //   provide: TOOLBAR_ACTION_CONTRIBUTORS,
        //   useValue: options.toolbarActionContributors,
        // },
        {
          provide: Toolbar_Action_Contributors,
          useValue: options.toolbarActionContributors,
        },
        {
          provide: Entity_Action_Contributors,
          useValue: options.entityActionContributors,
        },
        {
          provide: Entity_Props_Contributors,
          useValue: options.entityPropContributors,
        },
      ],
    };
  }
  static forLazy(options: any = {}): NgModuleFactory<CmsKitModule> {
    return new LazyModuleFactory(CmsKitModule.forChild(options));
  }
}
