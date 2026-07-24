
- [日本語](README.ja.md)

- [简体中文](README.zh_Hans.md)

- [繁體中文](README.zh_Hant.md)

# Dignite ABP

> [!WARNING]
> **This repository has been archived and is no longer actively maintained.**
>
> This project was hand-coded over several years around an idealistic vision for its features, but the implementation gradually fell behind that vision from an engineering standpoint. Over the past few months I've been using AI coding tools extensively in my day-to-day work, and it's become clear that AI's coding ability now far exceeds what I can produce by hand.
>
> This project was also structured in imitation of the [ABP Framework](https://abp.io/)'s architecture, which made it fairly large and heavyweight overall. Going forward, the genuinely valuable ABP modules from this codebase are moving to the new [dignite-projects/abp-modules](https://github.com/dignite-projects/abp-modules) repository, where they are developed and released together as lightweight, independently installable modules.
>
> Modules migrated so far:
>
> - **File Storing / File Explorer** → [dignite-projects/abp-modules/file-storing](https://github.com/dignite-projects/abp-modules/tree/main/file-storing)
> - **Notifications / Notification Center** → [dignite-projects/abp-modules/notifications](https://github.com/dignite-projects/abp-modules/tree/main/notifications)
>
> More modules will be extracted over time — stay tuned. This repository remains available for reference but will not receive further feature development.
>
> Thank you to everyone who has followed, used, or contributed to this project!

Empowering the [ABP Framework](https://abp.io/) ecosystem with enhancements such as notification systems, dynamic forms, user points, file management, CMS, multi-tenant themes, multi-tenant localization, multi-tenant domain names, and multi-tenant regionalization.

## Dignite ABP Modules

### Notification System

Inspired by the Asp.Net Boilerplate notification system, Dignite ABP provides a module for real-time system notifications, email notifications, and extensibility for custom notification methods through interface implementation.

- [Documentation](https://learn.dignite.com/en/abp/latest/Notifications)

- [Sample](https://github.com/dignite-projects/dignite-abp/tree/main/samples/NotificationCenterSample)

### Dynamic Forms

Dynamic forms allow administrators to define fields for business object entities dynamically online, widely applicable in e-commerce product SKUs, voting systems, CMS, and more.

- [Documentation](https://learn.dignite.com/en/abp/latest/Dynamic-Forms)

### Points System

The points system enhances user engagement, builds loyalty, and motivates active participation and contribution. It can be applied across e-commerce, social media, gaming, education, healthcare, and more.

- [Documentation](https://learn.dignite.com/en/abp/latest/Points)

### File Management

Dignite ABP Files, based on ABP BlobStoring, offers file type and size validation for uploads. Developers can also extend additional processing events.

- [Documentation](https://learn.dignite.com/en/abp/latest/File-Explorer)

- [Sample](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample)

### CMS

Dignite CMS is a CMS solution based on the ABP Framework. It enables developers to define fields online, addressing complex content display requirements for frontend pages. Dignite CMS supports multi-site, multilingual features, and provides highly flexible, comprehensive functionalities for diverse clients.

- [Documentation](https://dignite.com/docs/abp/latest/Cms/Index)

### Multi-Tenancy Features

#### Multi-Tenant Themes

Developers can customize unique ASP.NET MVC view UIs for each tenant.

- [Documentation](https://learn.dignite.com/en/abp/latest/Tenant-Theme)

#### Multi-Tenant Localization

Provides independent localization support for each tenant, personalizing content presentation.

- [Documentation](https://learn.dignite.com/en/abp/latest/Tenant-Localization)

#### Multi-Tenant Regionalization

For tenant websites targeting different regional users, this module provides default regions and supports multiple optional regions.

#### Multi-Tenant Domain Names

In SaaS applications, tenants may need to bind their own unique domain names. The multi-tenant domain name module enables independent domain name resolution for tenants.

### Pure Theme

A theme package developed by the Dignite ABP team for ABP, available in both Blazor and MVC versions. The Blazor version is based on BlazoriseUI, while the MVC version is built with Bootstrap.

- [Documentation](https://learn.dignite.com/en/abp/latest/Pure-Theme)

- [Sample](https://github.com/dignite-projects/dignite-abp/tree/main/modules/pure-theme)

### BlazoriseUI Components

A series of Blazor components developed based on Blazorise, including drag-and-drop tree components and enhanced DataGrid features.

- [Documentation](https://learn.dignite.com/en/abp/latest/BlazoriseUI-Component)

### CKEditor Component

A CKEditor component for ASP.NET Blazor, supporting both server-side and WebAssembly modes. Additionally, it provides a [Dynamic Forms](https://learn.dignite.com/en/abp/latest/Dynamic-Forms) component.

- [Documentation](https://learn.dignite.com/en/abp/latest/Blazor-Ckeditor-Component)

## Want to Contribute?

Dignite ABP is an open-source project built on [ABP Framework](https://github.com/abpframework) and licensed under LGPL-3.0. Contributions are welcome!

If you want to be part of this project, see our [Contribution Guide](https://learn.dignite.com/en/abp/latest/Contribution/Index).

## Official Links

- <a href="https://dignite.com/dignite-abp" target="_blank">Official Website</a>

- <a href="https://learn.dignite.com/en/abp" target="_blank">Documentation</a>

## Support Dignite ABP

Love the ABP Framework? **Star this repository** :star:

## Additional Services

- **Technical Support**  
  We provide remote technical guidance through email, forums, and other channels.

- **ABP Training**  
  We offer training services on the ABP Framework and related technologies for your development team.

- **Development Consulting**  
  Our expertise in ABP can assist with project planning, requirement analysis, code review, and more!

- **ABP Module Development**  
  Custom module development tailored to your needs – our specialty!

- **Migration to ABP Platform**  
  Migrate existing projects to the ABP platform to leverage its powerful features for better competitiveness, maintainability, and scalability.

## Contact Us

- **Company Name**  
  株式会社ディグナイト

- **Office Address**  
  3-11-23 Imazato, Higashinari-ku, Osaka, Japan

- **Contact**  
  <hello@dignite.com>
