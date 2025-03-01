import { EnvironmentService } from '@abp/ng.core';
import { inject, Pipe, PipeTransform } from '@angular/core';
/**获取当前窗口域名
 * 需要将environment.ts页面配置
  application: {
      baseUrl,
      name: 'Travely',
      webUrl:'https://localhost:44374',
    },
 *
 */
@Pipe({
  name: 'getNowDomain',
  standalone: true
})
export class GetNowDomainPipe implements PipeTransform {
  private environment = inject(EnvironmentService);

  transform(value: unknown, ...args: unknown[]): unknown {
    let production = this.environment.getEnvironment()?.production;
    let application:any=this.environment.getEnvironment()?.application;
    let webUrl=application?.webUrl;
    if (!production) return webUrl + value;
    return this.get(value);
  }

  /**获取当前域名并重定向链接 */
  get(value) {
    const baseUrl: any = (function () {
      let extractDomain = (url) => {
        const regex = /^(https?:\/\/)?([^\/]+)(?:[\/?].*)?$/;
        const match = url.match(regex);
        if (match) {
          const protocol = match[1] || '';
          const domain = `${protocol}${match[2]}`;
          return `${domain}`;
        }
        return url;
      }
      let url = window.location.href;
      return `${extractDomain(url)}` + value
    })()
    return baseUrl
  }

}
