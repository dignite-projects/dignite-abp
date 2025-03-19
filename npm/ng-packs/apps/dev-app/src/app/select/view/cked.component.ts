import { Component, Input } from '@angular/core';
import { ClassicEditor, Essentials, Paragraph, Bold, Italic } from 'ckeditor5';

@Component({
  selector: 'app-cked',
  templateUrl: './cked.component.html',
  styleUrl: './cked.component.scss'
})
export class CkedComponent {
  @Input() form: any;
  public Editor = ClassicEditor;
  public config = {
    plugins: [Essentials, Paragraph, Bold, Italic],
    toolbar: ['undo', 'redo', '|', 'bold', 'italic', '|', 'formatPainter'],
  };
}
