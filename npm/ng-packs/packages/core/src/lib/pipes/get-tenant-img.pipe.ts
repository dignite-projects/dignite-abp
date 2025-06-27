//pipe下新建：get-tenant-img.pipe.ts，
//如何引用见：独立管道的使用

import { ConfigStateService, EnvironmentService } from '@abp/ng.core';
import { inject, Pipe, PipeTransform } from '@angular/core';

/**获取租户图片 */
@Pipe({
  name: 'getTenantImg',
  standalone:true,
})
export class GetTenantImgPipe implements PipeTransform {
  private configState = inject(ConfigStateService)
  private environment = inject(EnvironmentService)
  transform(value: unknown, ...args: unknown[]): unknown {
    const tenantId = this.configState.getDeep('currentUser.tenantId')||'';
    const environment = this.environment.getEnvironment();
    const imgUrl =`${environment.apis.default.url}/api/file-explorer/files/${args}/${value}?__tenant=${tenantId}`
    return value?imgUrl:'';
  }
}
//使用
// {{'agentAvatarBlobName'|getTenantImg:'Avatar'}}
