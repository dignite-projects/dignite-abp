import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantDomainComponent } from './tenant-domain.component';

describe('TenantDomainComponent', () => {
  let component: TenantDomainComponent;
  let fixture: ComponentFixture<TenantDomainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TenantDomainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TenantDomainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
