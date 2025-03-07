/* eslint-disable @typescript-eslint/no-unused-vars */
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'slugNormalizer',
  standalone: true
})
export class SlugNormalizerPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return this.normalize(value);
  }

  normalize(value) {
    // Convert to lowercase
    value = value.toLowerCase();
    // Replace special characters with hyphen
    const invalidChars = ['\\', '/', '?', '&', '=', '+', '%', '#', '@', '!', '$', '\'', '"', ':', ';', '>', '<', '*', '(', ')', '[', '],', '{', '}', '|', '^', '`', '~'];
    for (const c of invalidChars) {
      value = value.replaceAll(c, '-');
    }
    // Convert spaces to hyphens
    value = value.replaceAll(" ", "-");

    // Remove multiple consecutive hyphens
    while (value.includes("--")) {
      value = value.replaceAll("--", "-");
    }

    // Trim hyphens from start and end
    value = value.trim('-');
    
    // URL encode the remaining string (handles UTF-8 characters)
    //部分编码-保留语义
    //return encodeURI(value);
    //全部编码
    return encodeURIComponent(value);
  }

}
