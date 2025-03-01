import { inject, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
  name: 'setCkeditorContent',
  standalone: true
})
export class SetCkeditorContentPipe implements PipeTransform {
  private sanitized = inject(DomSanitizer)
  transform(value: any, ...args: unknown[]): unknown {
    if(value){
      value = value.replace(/<oembed url/ig, "<iframe   src").replace(/oembed>/ig, "iframe>")
      value = value.replace(/<figure class="media"/ig, "<div").replace(/figure>/ig, "div>")
    }
    return this.sanitized.bypassSecurityTrustHtml(value);
  }

}
