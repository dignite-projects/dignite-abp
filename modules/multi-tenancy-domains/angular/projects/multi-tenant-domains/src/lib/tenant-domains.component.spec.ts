import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { TenantDomainsComponent } from './components/multi-tenancy-domains.component';
import { TenantDomainsService } from '@dignite.Abp/multi-tenancy-domains';
import { of } from 'rxjs';

describe('TenantDomainsComponent', () => {
  let component: TenantDomainsComponent;
  let fixture: ComponentFixture<TenantDomainsComponent>;
  const mockTenantDomainsService = jasmine.createSpyObj('TenantDomainsService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [TenantDomainsComponent],
      providers: [
        {
          provide: TenantDomainsService,
          useValue: mockTenantDomainsService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenantDomainsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
