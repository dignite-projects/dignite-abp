import { inject, Pipe, PipeTransform } from '@angular/core';
import { SectionType } from '../proxy/dignite/cms/sections';
import { EntryAdminService } from '../proxy/dignite/cms/admin/entries';

@Pipe({
  name: 'getLocalizedEntriesBySlug',
  standalone: true,
})
export class GetLocalizedEntriesBySlugPipe implements PipeTransform {
  private _EntryAdminService = inject(EntryAdminService);

  async transform(value: any, ...args: any[]): Promise<any> {
    const { slug, sectionType } = value as any;
    // if (sectionType == SectionType.Single) {
      //获取别名下其他的语言版本
       const Entries:any= await this.getLocalizedEntriesBySlug(args[0], slug);
      //判断Entries中是否还有未创建的语言版本，如果有则返回false，所有的语言版本args[1]都创建了则返回true，
      const state = args[1].every(el => {
        if (Entries.find(el2 => el2.culture === el.cultureName)) {
          return true;
        }
      });
      return state;
    // }
    // return true;
  }

  /**获取别名下其他的语言版本 */
  getLocalizedEntriesBySlug(sectionId, slug) {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.getLocalizedEntriesBySlug(sectionId, slug).subscribe(
        res => {
          resolve(res.items);
        },
        err => {
          resolve([]);
        },
      );
    });
  }
}
