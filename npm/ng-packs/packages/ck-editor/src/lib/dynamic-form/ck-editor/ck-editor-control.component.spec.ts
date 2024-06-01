import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkEditorControlComponent } from './ck-editor-control.component';

describe('CkEditorControlComponent', () => {
  let component: CkEditorControlComponent;
  let fixture: ComponentFixture<CkEditorControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CkEditorControlComponent]
    });
    fixture = TestBed.createComponent(CkEditorControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
