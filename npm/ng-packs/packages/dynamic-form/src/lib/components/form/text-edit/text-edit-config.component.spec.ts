import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextEditConfigComponent } from './text-edit-config.component';

describe('TextEditConfigComponent', () => {
  let component: TextEditConfigComponent;
  let fixture: ComponentFixture<TextEditConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextEditConfigComponent]
    });
    fixture = TestBed.createComponent(TextEditConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
