import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'getDirectoryName',
  standalone: true
})
export class GetDirectoryNamePipe implements PipeTransform {

  transform(value: unknown, ...args: any[]): unknown {
    if(value){
      return args[0].find(el=>el.id==value)?.name
    }
    return '';
  }

}
