import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileExplorerConfigComponent } from './file-explorer-config.component';

describe('FileExplorerConfigComponent', () => {
  let component: FileExplorerConfigComponent;
  let fixture: ComponentFixture<FileExplorerConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileExplorerConfigComponent]
    });
    fixture = TestBed.createComponent(FileExplorerConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
