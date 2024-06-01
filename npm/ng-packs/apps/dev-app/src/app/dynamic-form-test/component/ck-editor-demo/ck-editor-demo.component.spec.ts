import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkEditorDemoComponent } from './ck-editor-demo.component';

describe('CkEditorDemoComponent', () => {
  let component: CkEditorDemoComponent;
  let fixture: ComponentFixture<CkEditorDemoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CkEditorDemoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CkEditorDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
