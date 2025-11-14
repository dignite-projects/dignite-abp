import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreeConfigComponent } from './tree-config.component';

describe('TreeConfigComponent', () => {
  let component: TreeConfigComponent;
  let fixture: ComponentFixture<TreeConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TreeConfigComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TreeConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
