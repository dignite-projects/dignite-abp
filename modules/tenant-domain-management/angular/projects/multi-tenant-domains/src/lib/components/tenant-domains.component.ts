import { Component, OnInit } from '@angular/core';
import { TenantDomainsService } from '../services/tenant-domain-management.service';

@Component({
  selector: 'lib-tenant-domain-management',
  template: ` <p>tenant-domain-management works!</p> `,
  styles: [],
})
export class TenantDomainsComponent implements OnInit {
  constructor(private service: TenantDomainsService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
