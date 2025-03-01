import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileExplorerViewComponent } from './file-explorer-view.component';

describe('FileExplorerViewComponent', () => {
  let component: FileExplorerViewComponent;
  let fixture: ComponentFixture<FileExplorerViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FileExplorerViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FileExplorerViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
