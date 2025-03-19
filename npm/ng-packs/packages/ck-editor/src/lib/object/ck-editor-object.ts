/* eslint-disable no-var */

export var Simple: any = {
  toolbar: {
    items: [
      'undo',
      'redo',
      '|',
      'heading',
      '|',
      'link',
      'bold',
      'italic',
      'underline',
      'insertImage',
      '|',
      'bulletedList',
      'numberedList',
    ],
    shouldNotGroupWhenFull: false,
  },
  plugins: [],
  heading: {
    options: [
      {
        model: 'paragraph',
        title: 'Paragraph',
        class: 'ck-heading_paragraph',
      },
      {
        model: 'heading1',
        view: 'h1',
        title: 'Heading 1',
        class: 'ck-heading_heading1',
      },
      {
        model: 'heading2',
        view: 'h2',
        title: 'Heading 2',
        class: 'ck-heading_heading2',
      },
      {
        model: 'heading3',
        view: 'h3',
        title: 'Heading 3',
        class: 'ck-heading_heading3',
      },
      {
        model: 'heading4',
        view: 'h4',
        title: 'Heading 4',
        class: 'ck-heading_heading4',
      },
      {
        model: 'heading5',
        view: 'h5',
        title: 'Heading 5',
        class: 'ck-heading_heading5',
      },
      {
        model: 'heading6',
        view: 'h6',
        title: 'Heading 6',
        class: 'ck-heading_heading6',
      },
    ],
  },
  link: {
    addTargetToExternalLinks: true,
    defaultProtocol: 'https://',
    decorators: {
      toggleDownloadable: {
        mode: 'manual',
        label: 'Downloadable',
        attributes: {
          download: 'file',
        },
      },
    },
  },
};

// export var defaults: any= {
//     toolbar: {
//         items: [
//             'undo',
//             'redo',
//             '|',
//             'heading',
//             '|',
//             'fontSize',
//             'fontFamily',
//             'fontColor',
//             'fontBackgroundColor',
//             'link',
//             '|',
//             'bold',
//             'italic',
//             'underline',
//             '|',
//             'insertImage',
//         ],
//         shouldNotGroupWhenFull: false,
//     },
//     plugins: [],
//     heading: {
//         options: [
//             {
//                 model: 'paragraph',
//                 title: 'Paragraph',
//                 class: 'ck-heading_paragraph',
//             },
//             {
//                 model: 'heading1',
//                 view: 'h1',
//                 title: 'Heading 1',
//                 class: 'ck-heading_heading1',
//             },
//             {
//                 model: 'heading2',
//                 view: 'h2',
//                 title: 'Heading 2',
//                 class: 'ck-heading_heading2',
//             },
//             {
//                 model: 'heading3',
//                 view: 'h3',
//                 title: 'Heading 3',
//                 class: 'ck-heading_heading3',
//             },
//             {
//                 model: 'heading4',
//                 view: 'h4',
//                 title: 'Heading 4',
//                 class: 'ck-heading_heading4',
//             },
//             {
//                 model: 'heading5',
//                 view: 'h5',
//                 title: 'Heading 5',
//                 class: 'ck-heading_heading5',
//             },
//             {
//                 model: 'heading6',
//                 view: 'h6',
//                 title: 'Heading 6',
//                 class: 'ck-heading_heading6',
//             },
//         ],
//     },
//     fontSize: {
//         options: [10, 12, 14, 'default', 18, 20, 22, 24, 26, 28, 30, 32],
//     },
//     link: {
//         addTargetToExternalLinks: true,
//         defaultProtocol: 'https://',
//         decorators: {
//             toggleDownloadable: {
//                 mode: 'manual',
//                 label: 'Downloadable',
//                 attributes: {
//                     download: 'file',
//                 },
//             },
//         },
//     },
//     image: {
//         toolbar: [
//             'toggleImageCaption',
//             'imageTextAlternative',
//             '|',
//             'imageStyle:inline',
//             'imageStyle:wrapText',
//             'imageStyle:breakText',
//             '|',
//             'resizeImage',
//         ],
//         styles: {
//             options: [
//                 'inline', 'alignLeft', 'alignRight',
//                 'alignCenter', 'alignBlockLeft', 'alignBlockRight',
//                 'block', 'side'
//             ]
//         },
//         resizeOptions: [{
//             name: 'resizeImage:original',
//             label: 'Original',
//             value: null
//         },
//         {
//             name: 'resizeImage:25',
//             label: '25%',
//             value: '25'
//         },
//         {
//             name: 'resizeImage:50',
//             label: '50%',
//             value: '50'
//         },
//         {
//             name: 'resizeImage:75',
//             label: '75%',
//             value: '75'
//         }
//         ],
//     },
// }

// export var standard: any= {
//     toolbar: {
//         items: [
//             'undo',
//             'redo',
//             '|',
//             'heading',
//             '|',
//             'fontSize',
//             'fontFamily',
//             'fontColor',
//             'fontBackgroundColor',
//             '|',
//             'bold',
//             'italic',
//             'underline',
//             'strikethrough',
//             '|',
//             'link',
//             'insertImage',
//             'insertTable',
//             '|',
//             'alignment',
//             '|',
//             'bulletedList',
//             'numberedList',
//         ],
//         shouldNotGroupWhenFull: false,
//     },
//     plugins: [],
//     fontSize: {
//         options: [10, 12, 14, 'default', 18, 20, 22, 24, 26, 28, 30, 32],
//         supportAllValues: true,
//     },
//     heading: {
//         options: [
//             {
//                 model: 'paragraph',
//                 title: 'Paragraph',
//                 class: 'ck-heading_paragraph',
//             },
//             {
//                 model: 'heading1',
//                 view: 'h1',
//                 title: 'Heading 1',
//                 class: 'ck-heading_heading1',
//             },
//             {
//                 model: 'heading2',
//                 view: 'h2',
//                 title: 'Heading 2',
//                 class: 'ck-heading_heading2',
//             },
//             {
//                 model: 'heading3',
//                 view: 'h3',
//                 title: 'Heading 3',
//                 class: 'ck-heading_heading3',
//             },
//             {
//                 model: 'heading4',
//                 view: 'h4',
//                 title: 'Heading 4',
//                 class: 'ck-heading_heading4',
//             },
//             {
//                 model: 'heading5',
//                 view: 'h5',
//                 title: 'Heading 5',
//                 class: 'ck-heading_heading5',
//             },
//             {
//                 model: 'heading6',
//                 view: 'h6',
//                 title: 'Heading 6',
//                 class: 'ck-heading_heading6',
//             },
//         ],
//     },
//     htmlSupport: {
//         allow: [
//             {
//                 name: /^.*$/,
//                 styles: true,
//                 attributes: true,
//                 classes: true,
//             },
//         ],
//     },
//     image: {
//         toolbar: [
//             'toggleImageCaption',
//             'imageTextAlternative',
//             '|',
//             'imageStyle:inline',
//             'imageStyle:wrapText',
//             'imageStyle:breakText',
//             '|',
//             'resizeImage',
//         ],
//         styles: {
//             options: [
//                 'inline', 'alignLeft', 'alignRight',
//                 'alignCenter', 'alignBlockLeft', 'alignBlockRight',
//                 'block', 'side'
//             ]
//         },
//         resizeOptions: [{
//             name: 'resizeImage:original',
//             label: 'Original',
//             value: null
//         },
//         {
//             name: 'resizeImage:25',
//             label: '25%',
//             value: '25'
//         },
//         {
//             name: 'resizeImage:50',
//             label: '50%',
//             value: '50'
//         },
//         {
//             name: 'resizeImage:75',
//             label: '75%',
//             value: '75'
//         }
//         ],
//     },
//     link: {
//         addTargetToExternalLinks: true,
//         defaultProtocol: 'https://',
//         decorators: {
//             toggleDownloadable: {
//                 mode: 'manual',
//                 label: 'Downloadable',
//                 attributes: {
//                     download: 'file',
//                 },
//             },
//         },
//     },
//     list: {
//         properties: {
//             styles: true,
//             startIndex: true,
//             reversed: true,
//         },
//     },
//     style: {
//         definitions: [
//             {
//                 name: 'Article category',
//                 element: 'h3',
//                 classes: ['category'],
//             },
//             {
//                 name: 'Title',
//                 element: 'h2',
//                 classes: ['document-title'],
//             },
//             {
//                 name: 'Subtitle',
//                 element: 'h3',
//                 classes: ['document-subtitle'],
//             },
//             {
//                 name: 'Info box',
//                 element: 'p',
//                 classes: ['info-box'],
//             },
//             {
//                 name: 'Side quote',
//                 element: 'blockquote',
//                 classes: ['side-quote'],
//             },
//             {
//                 name: 'Marker',
//                 element: 'span',
//                 classes: ['marker'],
//             },
//             {
//                 name: 'Spoiler',
//                 element: 'span',
//                 classes: ['spoiler'],
//             },
//             {
//                 name: 'Code (dark)',
//                 element: 'pre',
//                 classes: ['fancy-code', 'fancy-code-dark'],
//             },
//             {
//                 name: 'Code (bright)',
//                 element: 'pre',
//                 classes: ['fancy-code', 'fancy-code-bright'],
//             },
//         ],
//     },
//     table: {
//         contentToolbar: [
//             'tableColumn',
//             'tableRow',
//             'mergeTableCells',
//             'tableProperties',
//             'tableCellProperties',
//         ],
//     },
// }
export var Classic: any = {
  toolbar: {
    items: [
      'undo',
      'redo',
      '|',
      'heading',
      '|',
      'fontSize',
      'fontFamily',
      'fontColor',
      'fontBackgroundColor',
      '|',
      'bold',
      'italic',
      'underline',
      'strikethrough',
      'subscript',
      'link',
      '|',
      'highlight',
      'blockQuote',
      '|',
      'superscript',
      'code',
      'removeFormat',
      '|',
      'insertImage',
      'mediaEmbed',
      'insertTable',
      '|',
      'alignment',
      'bulletedList',
      'numberedList',
      'todoList',
      'outdent',
      'indent',
    ],
    shouldNotGroupWhenFull: false,
  },
  plugins: [],
  fontFamily: {
    supportAllValues: true,
  },
  fontSize: {
    options: [10, 12, 14, 'default', 18, 20, 22, 24, 26, 28, 30, 32],
    supportAllValues: true,
  },
  heading: {
    options: [
      {
        model: 'paragraph',
        title: 'Paragraph',
        class: 'ck-heading_paragraph',
      },
      {
        model: 'heading1',
        view: 'h1',
        title: 'Heading 1',
        class: 'ck-heading_heading1',
      },
      {
        model: 'heading2',
        view: 'h2',
        title: 'Heading 2',
        class: 'ck-heading_heading2',
      },
      {
        model: 'heading3',
        view: 'h3',
        title: 'Heading 3',
        class: 'ck-heading_heading3',
      },
      {
        model: 'heading4',
        view: 'h4',
        title: 'Heading 4',
        class: 'ck-heading_heading4',
      },
      {
        model: 'heading5',
        view: 'h5',
        title: 'Heading 5',
        class: 'ck-heading_heading5',
      },
      {
        model: 'heading6',
        view: 'h6',
        title: 'Heading 6',
        class: 'ck-heading_heading6',
      },
    ],
  },
  image: {
    toolbar: [
      'toggleImageCaption',
      'imageTextAlternative',
      '|',
      'imageStyle:inline',
      'imageStyle:wrapText',
      'imageStyle:breakText',
      '|',
      'resizeImage',
    ],
    styles: {
      options: [
        'inline',
        'alignLeft',
        'alignRight',
        'alignCenter',
        'alignBlockLeft',
        'alignBlockRight',
        'block',
        'side',
      ],
    },
    resizeOptions: [
      {
        name: 'resizeImage:original',
        label: 'Original',
        value: null,
      },
      {
        name: 'resizeImage:25',
        label: '25%',
        value: '25',
      },
      {
        name: 'resizeImage:50',
        label: '50%',
        value: '50',
      },
      {
        name: 'resizeImage:75',
        label: '75%',
        value: '75',
      },
    ],
  },
  link: {
    addTargetToExternalLinks: true,
    defaultProtocol: 'https://',
    decorators: {
      toggleDownloadable: {
        mode: 'manual',
        label: 'Downloadable',
        attributes: {
          download: 'file',
        },
      },
    },
  },
  list: {
    properties: {
      styles: true,
      startIndex: true,
      reversed: true,
    },
  },
  style: {
    definitions: [
      {
        name: 'Article category',
        element: 'h3',
        classes: ['category'],
      },
      {
        name: 'Title',
        element: 'h2',
        classes: ['document-title'],
      },
      {
        name: 'Subtitle',
        element: 'h3',
        classes: ['document-subtitle'],
      },
      {
        name: 'Info box',
        element: 'p',
        classes: ['info-box'],
      },
      {
        name: 'Side quote',
        element: 'blockquote',
        classes: ['side-quote'],
      },
      {
        name: 'Marker',
        element: 'span',
        classes: ['marker'],
      },
      {
        name: 'Spoiler',
        element: 'span',
        classes: ['spoiler'],
      },
      {
        name: 'Code (dark)',
        element: 'pre',
        classes: ['fancy-code', 'fancy-code-dark'],
      },
      {
        name: 'Code (bright)',
        element: 'pre',
        classes: ['fancy-code', 'fancy-code-bright'],
      },
    ],
  },
  table: {
    contentToolbar: [
      'tableColumn',
      'tableRow',
      'mergeTableCells',
      'tableProperties',
      'tableCellProperties',
    ],
  },
};
export var CkEditorTypesObject = {
  // default: defaults,
  Simple: Simple,
  // standard: standard,
  Classic: Classic,
};
