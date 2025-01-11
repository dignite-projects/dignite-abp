import { Component, OnInit } from '@angular/core';
import { TenantDomainsService } from '../services/tenant-domains.service';

@Component({
  selector: 'lib-tenant-domains',
  template: ` <p>tenant-domains works!</p> `,
  styles: [],
})
export class TenantDomainsComponent implements OnInit {
  constructor(private service: TenantDomainsService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
