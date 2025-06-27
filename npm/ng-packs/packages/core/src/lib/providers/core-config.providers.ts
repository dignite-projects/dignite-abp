import { makeEnvironmentProviders } from '@angular/core';
import { CORE_STYLES_PROVIDERS } from './styles.providers';
import { RouteReuseStrategy } from '@angular/router';
import { SimpleReuseStrategy } from '../strategies';

/**
 * 用于导入providers依赖
 */
export function provideCoreConfig() {
  return makeEnvironmentProviders([
    CORE_STYLES_PROVIDERS,
    //路由复用策略
    { provide: RouteReuseStrategy, useClass: SimpleReuseStrategy }
  ]);
}
