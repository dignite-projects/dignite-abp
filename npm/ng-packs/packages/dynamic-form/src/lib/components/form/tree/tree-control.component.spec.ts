import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreeControlComponent } from './tree-control.component';

describe('TreeControlComponent', () => {
  let component: TreeControlComponent;
  let fixture: ComponentFixture<TreeControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TreeControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TreeControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
