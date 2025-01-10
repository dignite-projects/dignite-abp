import { Component, OnInit } from '@angular/core';
import { RegionalizationManagementService } from '../services/regionalization-management.service';

@Component({
  selector: 'lib-regionalization-management',
  template: ` <p>regionalization-management works!</p> `,
  styles: [],
})
export class RegionalizationManagementComponent implements OnInit {
  constructor(private service: RegionalizationManagementService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
