import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantDomainManagementComponent } from './tenant-domain-management.component';

describe('TenantDomainManagementComponent', () => {
  let component: TenantDomainManagementComponent;
  let fixture: ComponentFixture<TenantDomainManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TenantDomainManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TenantDomainManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
