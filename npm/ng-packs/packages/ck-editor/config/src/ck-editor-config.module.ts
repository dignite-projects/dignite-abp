import { ModuleWithProviders, NgModule } from '@angular/core';
import { ck_editor_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class CkEditorConfigModule {
  static forRoot(): ModuleWithProviders<CkEditorConfigModule> {
    return {
      ngModule: CkEditorConfigModule,
      providers: [ck_editor_ROUTE_PROVIDERS],
    };
  }
}
