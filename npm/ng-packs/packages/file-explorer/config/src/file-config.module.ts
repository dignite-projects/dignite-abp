import { ModuleWithProviders, NgModule } from '@angular/core';
import { FILE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class FileConfigModule {
  static forRoot(): ModuleWithProviders<FileConfigModule> {
    return {
      ngModule: FileConfigModule,
      providers: [FILE_ROUTE_PROVIDERS],
    };
  }
}
