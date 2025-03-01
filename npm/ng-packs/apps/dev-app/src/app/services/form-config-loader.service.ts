import { Injectable } from '@angular/core';
// import { travelyFieldControlGroup } from '../travely/dynamic-form/form-control-group';

@Injectable({
  providedIn: 'root',
})
export class FormConfigLoaderService {
  private configs: any[] = [];

  constructor() {}

  // 动态加载配置
  async loadConfig(): Promise<void> {
    const ckEditorFieldControlGroup = await import('@dignite-ng/expand.ck-editor').then(
      module => module.ckEditorFieldControlGroup,
    );
    const fielFieldControlGroup = await import('@dignite-ng/expand.file-explorer').then(
      module => module.fielFieldControlGroup,
    );
    const cmsFieldControlGroup = await import('@dignite-ng/expand.cms').then(
      module => module.cmsFieldControlGroup,
    );
    this.configs.push(
      ...cmsFieldControlGroup,
      ...ckEditorFieldControlGroup,
      ...fielFieldControlGroup,
    );
  }

  // 合并配置
  getMergedConfig() {
    return this.configs;
  }
}
