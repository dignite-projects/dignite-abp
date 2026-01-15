/* eslint-disable no-async-promise-executor */
import { Injectable } from '@angular/core';
import { CkEditorTypesObject } from '../object/ck-editor-object';
import { LanguagesMap } from '../enums/languages-map';
import { CkEditorModeEnum } from '../enums/ck-editor-mode.enum';
import { PasteFromMarkdownExperimental } from 'ckeditor5';
// import { CkEditorModeEnum } from '../enums/ck-editor-mode-enum';

@Injectable({
  providedIn: 'root',
})
export class SetCkEditorConfigsService {
  async get(input) {
    const { language, type } = input;
    return new Promise(async (resolve, rejects) => {
      const configs: any = CkEditorTypesObject[CkEditorModeEnum[type]];
      const {
        AccessibilityHelp,
        Highlight,
        AutoLink,
        Alignment,
        Autoformat,
        AutoImage,
        Autosave,
        Base64UploadAdapter,
        BlockQuote,
        Bold,
        CloudServices,
        Code,
        Essentials,
        FontBackgroundColor,
        FontColor,
        FontFamily,
        FontSize,
        GeneralHtmlSupport,
        Heading,
        HorizontalLine,
        ImageBlock,
        ImageCaption,
        ImageInline,
        ImageInsert,
        ImageInsertViaUrl,
        ImageResize,
        ImageStyle,
        ImageTextAlternative,
        ImageToolbar,
        ImageUpload,
        Indent,
        IndentBlock,
        Italic,
        Link,
        LinkImage,
        List,
        ListProperties,
        MediaEmbed,
        Paragraph,
        PasteFromOffice,
        RemoveFormat,
        SelectAll,
        SpecialCharacters,
        SpecialCharactersArrows,
        SpecialCharactersCurrency,
        SpecialCharactersEssentials,
        SpecialCharactersLatin,
        SpecialCharactersMathematical,
        SpecialCharactersText,
        Strikethrough,
        Style,
        Subscript,
        Superscript,
        Table,
        TableCaption,
        TableCellProperties,
        TableColumnResize,
        TableProperties,
        TableToolbar,
        TextTransformation,
        TodoList,
        Underline,
        Undo,
        Clipboard,
      } = await import('ckeditor5');
      configs.plugins = [
        PasteFromMarkdownExperimental,
        Essentials,
        Clipboard,
        AccessibilityHelp,
        Highlight,
        Alignment,
        Autoformat,
        AutoImage,
        Autosave,
        Base64UploadAdapter,
        BlockQuote,
        Bold,
        CloudServices,
        Code,
        Essentials,
        // FontBackgroundColor,
        // FontColor,
        // FontFamily,
        // FontSize,
        GeneralHtmlSupport,
        Heading,
        HorizontalLine,
        ImageBlock,
        ImageCaption,
        ImageInline,
        ImageInsert,
        ImageInsertViaUrl,
        ImageResize,
        ImageStyle,
        ImageTextAlternative,
        ImageToolbar,
        ImageUpload,
        Indent,
        IndentBlock,
        Italic,
        Link,
        LinkImage,
        List,
        ListProperties,
        MediaEmbed,
        Paragraph,
        PasteFromOffice,
        RemoveFormat,
        SelectAll,
        SpecialCharacters,
        SpecialCharactersArrows,
        SpecialCharactersCurrency,
        SpecialCharactersEssentials,
        SpecialCharactersLatin,
        SpecialCharactersMathematical,
        SpecialCharactersText,
        Strikethrough,
        Style,
        Subscript,
        Superscript,
        Table,
        TableCaption,
        TableCellProperties,
        TableColumnResize,
        TableProperties,
        TableToolbar,
        TextTransformation,
        TodoList,
        Underline,
        Undo,
      ];

      configs.language = LanguagesMap[language];
      await import(
        `@ckeditor/ckeditor5-build-decoupled-document/build/translations/${LanguagesMap[language]}`
      );
      if (configs.toolbar.items.includes('removeFormat')) {
        await import(
          `@ckeditor/ckeditor5-remove-format/build/translations/${LanguagesMap[language]}`
        );
      }
      resolve(configs);
    });
  }
}
