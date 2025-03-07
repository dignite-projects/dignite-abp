import { Validators } from '@angular/forms';

export class TenantDomainFormConfig {

  domainName: any = ['', [Validators.required]];
}
