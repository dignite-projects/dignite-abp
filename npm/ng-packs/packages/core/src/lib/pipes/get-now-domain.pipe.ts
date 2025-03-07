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

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  transform(value: unknown, ...args: unknown[]): unknown {
    const production = this.environment.getEnvironment()?.production;
    const application:any=this.environment.getEnvironment()?.application;
    const webUrl=application?.webUrl;
    if (!production) return webUrl + value;
    return this.get(value);
  }

  /**获取当前域名并重定向链接 */
  get(value) {
    const baseUrl: any = (function () {
      const extractDomain = (url) => {
        const regex = /^(https?:\/\/)?([^/]+)(?:[/?].*)?$/;
        // const regex = /^(https?:\/\/)?([^\/]+)(?:[\/?].*)?$/;
        const match = url.match(regex);
        if (match) {
          const protocol = match[1] || '';
          const domain = `${protocol}${match[2]}`;
          return `${domain}`;
        }
        return url;
      }
      const url = window.location.href;
      return `${extractDomain(url)}` + value
    })()
    return baseUrl
  }

}
