# Blazor Ckeditor 组件

````json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
````

Ckeditor 的 Blazor 版,适用于Blazor server和 Blazor WebAssembly

![Blazor Ckeditor 组件](images/ckeditor.jpg)

## 安装

* 将 `Dignite.Abp.AspNetCore.Components.CkEditor` Nuget 包安装到 Blazor 项目中

{{if UI == "Blazor"}}

* 将 `Dignite.Abp.AspNetCore.Components.CkEditor.WebAssembly` NuGet 包安装到 Blazor WebAssembly Web 项目中。

* 添加 `DigniteAbpAspNetCoreComponentsCkEditorWebAssemblyModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 在Blazor项目中执行一次 [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle)

{{end}}

{{if UI == "BlazorServer"}}

* 将 `Dignite.Abp.AspNetCore.Components.CkEditor.Server` NuGet 包安装到 Blazor Server Web 项目中。

* 添加 `DigniteAbpAspNetCoreComponentsCkEditorServerModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中.

{{end}}

## 使用方法

在页面顶部引入如下命名空间

```csharp
@using Dignite.Abp.AspNetCore.Components.CkEditor
```

### 基于用法

```html
<CkEditor @bind-Content="Content">
</CkEditor>
```

```csharp
    public partial class TestCkEditor
    {
        // 获取或设置 Ckeditor 内容
        public string Content{ get; set; } = "CkEditor Default Content";
    }
```

### 上传图片

首先设置图片存储的容器，具体操作方法请参见 [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 和 [Dignite Abp Files](Files.md)。

> 本例假定已创建了名为 `TestPicStore` 的容器.

```html
<CkEditor @bind-Content="Content" ImagesContainerName="TestPicStore">
</CkEditor>
```

![Blazor Ckeditor 上传图片](images/ckeditor-insert-pic.jpg)

点击`插入图像`工具，选择本机文件将上传至名为 `TestPicStore` 的容器中，并插入编辑区。

### 插入视频媒体

![Blazor Ckeditor 上传图片](images/ckeditor-insert-media.jpg)

除`Ckeditor`默认支持的媒体外，本组件提供了`腾讯视频`、`优酷视频`、`西瓜视频`的支持。

#### 视频媒体解析

由于`Ckeditor`将插入的媒体转换为如下代码：

```html
<figure class="media"><oembed url="https://www.youtube.com/watch?v=Xf3ZUfESLeo"></oembed></figure>
```

因此需要在输出页面解析`Ckeditor`中的视频媒体，参考代码如下：

```javascript
/**
 * 解析ckeditor中引入的视频
 */
$(function () {
    // Select all <figure> elements with class "media"
    var figureElements = document.querySelectorAll('figure.media');
    var mediaEmbedProviders = [
        {
            name: 'ixigua',
            url: /^https:\/\/www\.ixigua\.com\/(\d+)(\?logTag=[\w\d]+)?/,
            html: match => {
                return `<iframe src='https://www.ixigua.com/iframe/${match[1]}?autoplay=0' title="Ixigua video player" allowFullScreen></iframe>`;
            }
        },
        {
            name: 'youtube',
            url: /https:\/\/www\.youtube\.com\/watch\?v=([^"']+)?/,
            html: match => {
                return `<iframe src="https://www.youtube.com/embed/${match[1]}" title="YouTube video player" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>`;
            }
        }
    ];


    // Loop through each <figure> element
    figureElements.forEach(function (figure) {
        var oembedElement = figure.firstChild;
        mediaEmbedProviders.forEach(function (provider) {
            var match = oembedElement.getAttribute('url').match(provider.url);
            if (match && match.length > 1) {
                var videoContainer = document.createElement('div');
                videoContainer.classList.add('ratio', 'ratio-16x9');
                videoContainer.innerHTML = provider.html(match);
                figure.appendChild(videoContainer);
            }
        });
    });
});
```
