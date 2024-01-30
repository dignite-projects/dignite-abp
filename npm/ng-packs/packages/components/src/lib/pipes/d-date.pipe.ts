import { Pipe, PipeTransform } from '@angular/core';
import { BaseService } from '../services';

@Pipe({
  name: 'd-Date'
})
export class DDatePipe implements PipeTransform {

  constructor(
		private _BaseService:BaseService
	){}

	transform(date: Date, type: string, ...args: unknown[]): unknown {

		return this._BaseService.setTime(date,type)
	}

}
