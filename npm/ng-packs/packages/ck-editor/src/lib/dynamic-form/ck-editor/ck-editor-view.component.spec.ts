import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkEditorViewComponent } from './ck-editor-view.component';

describe('CkEditorViewComponent', () => {
  let component: CkEditorViewComponent;
  let fixture: ComponentFixture<CkEditorViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CkEditorViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CkEditorViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
