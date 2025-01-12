import { Component, OnInit } from '@angular/core';
import { TenantDomainsService } from '../services/multi-tenancy-domains.service';

@Component({
  selector: 'lib-multi-tenancy-domains',
  template: ` <p>multi-tenancy-domains works!</p> `,
  styles: [],
})
export class TenantDomainsComponent implements OnInit {
  constructor(private service: TenantDomainsService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
