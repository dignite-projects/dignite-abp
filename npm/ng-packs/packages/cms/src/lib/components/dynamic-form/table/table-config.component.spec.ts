import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableConfigComponent } from './table-config.component';

describe('TableConfigComponent', () => {
  let component: TableConfigComponent;
  let fixture: ComponentFixture<TableConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableConfigComponent]
    });
    fixture = TestBed.createComponent(TableConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
