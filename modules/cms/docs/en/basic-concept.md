# Dignite Cms Basic Concepts

## Site Settings

Dignite Cms settings are based on the [Abp Settings](https://abp.io/docs/latest/framework/infrastructure/settings) module, which sets the language and branding information for the master and tenant sites.

Below is an example of basic settings for the master site:

```json
"Settings": {
  "Abp.Localization.DefaultLanguage": "en",
  "CmsSettings.Site.Languages": "en,ja,zh-Hant",
  "CmsSettings.Site.Name": "My Dignite", 
  "CmsSettings.Site.LogoUrl":"/images/logo.png",
  "CmsSettings.Site.LogoReverseUrl":"/images/logo-reverse.png"
}
```

See the [Setting Management](https://abp.io/docs/latest/modules/setting-management) documentation for tenant settings.

## Fields

Fields in Dignite CMS are implemented using [Dynamic Forms](https://learn.dignite.com/zh-Hans/abp/latest/Dynamic-Forms) in Dignite ABP. They allow developers to define fields online to meet various frontend content display needs.

**Example of a Text Edit Field:**

![Text Edit Field](images/textedit-field.png)

Common properties:

- Field Label: The display text of the field.
- Field Name: The unique identifier of the field.
- Field Description: The descriptive text of the field.
- Field Type: Various types of form fields are supported, including text box, dropdown menu, rich text editor, date picker, file upload, numeric box, etc.

Configuration for text edit fields:

- Placeholder for Text Edit Form: Text displayed in the text box before the user enters text.
- Mode of Text Edit Form: Includes single-line text box and multi-line text box modes.
- Character Limit for Text Edit Form: Limits the maximum number of characters that users can input.

### Advanced Forms

In addition to common types of dynamic forms, Dignite CMS also provides three advanced form types:

- Entry Selection: Configures the data source for a list of entries, allowing users to select entries.
- Matrix: Enables rich structured content management by configuring matrix blocks.
- Table: Standardizes user input data by configuring table headers.

> For more information on form usage, please refer to the quick start tutorial to launch Dignite CMS Blazor WebAssembly and experience it in the admin backend.

## Sections

Sections are the skeleton of a website, used to partition and block website content.

![Edit Section](images/section-edit.png)

- Section Type: Includes three types - Single Page, Structural, and Channel:
  
  - Single Page: Each section can have only one entry of the same entry type, used for website homepages, product index pages, blog homepages, etc.
  - Structural: Suitable for scenarios where developers anticipate that the number of entries in this type will not be too large and require manual sorting or multi-level structure, such as FAQs, service projects, etc.
  - Channel: Suitable for scenarios where new content is continuously published, supporting an unlimited number of entries, such as blogs, news, etc.

- Display Name: The display text of the section.
- Name: The name of the section, unique within the same site.
- Entry Route: Different entry route rules can be set based on different types of sections.
- Page Template: The view page path for displaying entries, without the .cshtml extension.
- Default: Specifies the default section when accessing the website, only applicable to single-page type sections.
- Active: Enables or disables user access to the section.
  
## Entry Types

Entries in sections can exist in various forms, such as text, images, videos, or galleries for blog sections. Each type can be configured with different fields to meet frontend UI requirements.

![Edit Entry Type](images/entry-type-edit.png)

- Display Name: The display text of the entry type.
- Name: The name of the entry type, unique within the same section.
- Field Layout: Fields can be laid out in multiple tabs, and support for setting field labels, whether they are required, and whether they should be included in lists.
- Advanced Forms: Supports advanced form types such as entry selection, matrix, and table.

## Entries

Entries are the content of website pages, and their data is composed of fields.

Entries belong to a specific section and support multilingual and multi-version features.

![Edit Entry](images/entry-edit.png)

- Title: Each entry has a title.
- Form Fields: Composed of fields defined in the entry type.
- Alias: Each entry has a unique alias within the section.
- Language: The language of the entry is configured by the site's language list.
- Publish Time: Defaults to the current time of entry creation and supports manually modifying the publish time.
- Version: Supports multi-version functionality, allowing multiple versions to be created for each entry, but only one version is active at a time.
- Revision Note: Records explanations for editing entries.
