# Dignite abp

为ABP框架生态添砖加瓦，增加通知系统、动态表单模块、文件管理器、Pure Theme，以及其他增强功能。

> Dignite Abp 3.0.3 是基于 Abp Framework 8.0.4 构建的，同时也是基于 .NET Framework 8.0 构建。为了顺利运行 Dignite Abp 3.0.3，请确保您的计算机上已安装了 .NET Framework 8.0。

## Dignite Abp 模块

### 通知

参考Asp.Net Boilerplate的通知系统，移植到Abp Framework。使用简单的代码发布通知，实现系统内实时通知，开发者还可以实现自己的通知方式。

- [文档](Notifications.md)

- [示例](https://github.com/dignite-projects/dignite-abp/tree/main/samples/NotificationCenterSample)

### 积分

积分能够增强用户参与度、建立忠诚度、激励用户积极参与和贡献，可广泛应用于电子商务、社交媒体、游戏、教育和健康等系统中。

- [文档](Points.md)

### 动态表单

动态表单可以使系统管理者在线动态自定义业务对象实体的字段，主要应用于商城系统中商品的SKU、投票调研系统、CMS等系统。

- [文档](Dynamic-Forms.md)

### 文件管理器

Dignite Abp Files 基于ABP BlobStoring开发，为文件上传过程提供文件类型验证、文件大小验证的处理，开发者还可以扩展更多的处理事件。

- [文档](File-Explorer.md)

- [示例](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample)

### 区域化

可以像微软官网一样构建不同区域的 Culture Url

- [文档](Regionalization.md)

### Pure Theme

由Dignite Abp团队开发的Abp主题包，包含Blazor版本和MVC版本。Blazor版基于BlazoriseUI，MVC版则基于Bootstrap。

- [文档](Pure-Theme.md)

- [示例](https://github.com/dignite-projects/dignite-abp/tree/main/modules/pure-theme)

### 租户主题

每一个租户都能有一套独立的视图 UI，开发者可轻松实现租户UI的个性化定制。

- [文档](Tenant-Theme.md)

### 租户本地化

每一个租户都能有一套独立的语言包，实现租户个性化内容呈现。

- [文档](Tenant-Localization.md)

### 租户域名

实现用户自主绑定域名

- [文档](Tenant-Domain.md)

### SEO

SEO

- [文档](seo.md)

### CMS

Dignite Cms是一个基于[Abp Framework](https://github.com/abpframework)开发的CMS模块，开发者通过自定义字段，满足前端页面数据展示的需求。

- [文档](cms/index.md)

### BlazoriseUI 组件

基于Blazorise开发的一系列Blazor 组件，包含支持拖拽的树开组件、增强功能的DataGrid等组件。

- [文档](BlazoriseUI-Component.md)

### Ckeditor 组件

应用于Asp.net Blazor的Ckeditor Component，支持Server模式和WebAssembly模式。另外，还适配了动态表单模块。

- [文档](Blazor-Ckeditor-Component.md)

## 想要贡献吗？

Dignite ABP是基于[Abp Framework](https://github.com/abpframework)构建的开源项目，早期供Dignite内部使用，现为正式开源，为ABP框架生态添砖加瓦。

如果你想成为这个项目的一员，请参阅[贡献指南](Contribution/Index.md)。
