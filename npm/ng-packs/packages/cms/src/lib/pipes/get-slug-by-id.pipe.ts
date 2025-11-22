import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'getSlugById',
  standalone: true
})
export class GetSlugByIdPipe implements PipeTransform {

  transform(parentId: string, items: any[]): string {
    return items?.find(item => item.id === parentId)?.slug || '';
  }

}
