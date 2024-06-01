import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkEditorBaseComponent } from './ck-editor-base.component';

describe('CkEditorBaseComponent', () => {
  let component: CkEditorBaseComponent;
  let fixture: ComponentFixture<CkEditorBaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CkEditorBaseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CkEditorBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
