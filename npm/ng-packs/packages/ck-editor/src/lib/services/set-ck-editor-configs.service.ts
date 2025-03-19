/* eslint-disable no-async-promise-executor */
import { Injectable } from '@angular/core';
import { CkEditorTypesObject } from '../object/ck-editor-object';
import { LanguagesMap } from '../enums/languages-map';
import { CkEditorModeEnum } from '../enums/ck-editor-mode.enum';
// import { CkEditorModeEnum } from '../enums/ck-editor-mode-enum';

@Injectable({
  providedIn: 'root',
})
export class SetCkEditorConfigsService {
  constructor() {}
  async get(input) {
    const { language,type } = input;
    return new Promise(async (resolve, rejects) => {
      const configs:any = CkEditorTypesObject[CkEditorModeEnum[type]]; 
      const {AccessibilityHelp,Highlight, Alignment, Autoformat, AutoImage, Autosave, Base64UploadAdapter, BlockQuote, Bold, CloudServices, Code, Essentials, FontBackgroundColor, FontColor, FontFamily, FontSize, GeneralHtmlSupport, Heading, HorizontalLine, ImageBlock, ImageCaption, ImageInline, ImageInsert, ImageInsertViaUrl, ImageResize, ImageStyle, ImageTextAlternative, ImageToolbar, ImageUpload, Indent, IndentBlock, Italic, Link, LinkImage, List, ListProperties, MediaEmbed, Paragraph, PasteFromOffice, RemoveFormat, SelectAll, SpecialCharacters, SpecialCharactersArrows, SpecialCharactersCurrency, SpecialCharactersEssentials, SpecialCharactersLatin, SpecialCharactersMathematical, SpecialCharactersText, Strikethrough, Style, Subscript, Superscript, Table, TableCaption, TableCellProperties, TableColumnResize, TableProperties, TableToolbar, TextTransformation, TodoList, Underline, Undo}=await import('ckeditor5');
      configs.plugins = [AccessibilityHelp,Highlight, Alignment, Autoformat, AutoImage, Autosave, Base64UploadAdapter, BlockQuote, Bold, CloudServices, Code, Essentials, FontBackgroundColor, FontColor, FontFamily, FontSize, GeneralHtmlSupport, Heading, HorizontalLine, ImageBlock, ImageCaption, ImageInline, ImageInsert, ImageInsertViaUrl, ImageResize, ImageStyle, ImageTextAlternative, ImageToolbar, ImageUpload, Indent, IndentBlock, Italic, Link, LinkImage, List, ListProperties, MediaEmbed, Paragraph, PasteFromOffice, RemoveFormat, SelectAll, SpecialCharacters, SpecialCharactersArrows, SpecialCharactersCurrency, SpecialCharactersEssentials, SpecialCharactersLatin, SpecialCharactersMathematical, SpecialCharactersText, Strikethrough, Style, Subscript, Superscript, Table, TableCaption, TableCellProperties, TableColumnResize, TableProperties, TableToolbar, TextTransformation, TodoList, Underline, Undo];
      configs.language = LanguagesMap[language];
      configs.forcePasteAsPlainText = true;
      await import(
        `@ckeditor/ckeditor5-build-decoupled-document/build/translations/${LanguagesMap[language]}`
      );
      resolve(configs);
    });
  }
}
