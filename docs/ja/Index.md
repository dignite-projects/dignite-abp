# Dignite abp

ABPフレームワークのエコロジーを加える, 通知システム、ダイナミック・フォーム・モジュール、ファイル・マネージャー、ピュア・テーマなどの機能強化。

> Dignite Abp 2.0.0はAbp Framework 7.4.5の上に構築されており、Abp Framework 7.4.5は.NET Framework 7.0に基づいています。Dignite Abp 2.0.0をスムーズに実行するために、お使いのコンピュータに.NET Framework 7.0がインストールされていることを確認してください。

## Dignite Abp Modules

### 通知システム

ASP.NET Boilerplateの通知システムをABP Frameworkに移植しました。簡単なコードを使用して通知を発行し、システム内でリアルタイム通知とメール通知を実現し、開発者は独自の通知方法を実装することもできます。

- [ドキュメント](Notifications.md)

- [サンプル](https://github.com/dignite-projects/dignite-abp/tree/main/samples/NotificationCenterSample)

### ポイント

ポイントは、ユーザーのエンゲージメントを高め、ロイヤリティを構築し、積極的な参加と貢献のインセンティブとなり、eコマース、ソーシャルメディア、ゲーム、教育、健康など幅広いシステムで利用できる。

- [ドキュメント](Points.md)

### 動的フォーム

ダイナミック・フォームは、システム管理者がビジネス・オブジェクト・エンティティのフィールドをオンラインで動的にカスタマイズできるようにするもので、主にショッピング・モール・システム、世論調査システム、CMS、その他のシステムの商品のSKUに適用される。

- [ドキュメント](Dynamic-Forms.md)

### 文書管理

Dignite Abp FilesはABP BlobStoringをベースに開発され、ファイルのアップロードプロセスでファイルの種類検証やファイルサイズ検証を提供します。開発者はさらに多くの処理イベントを拡張することもできます。

- [ドキュメント](File-Explorer.md)

- [サンプル](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample.md)

### Pure Theme

Dignite Abpチームによって開発されたAbpテーマパッケージには、BlazorバージョンとMVCバージョンが含まれています。BlazorバージョンはBlazoriseUIをベースにしており、MVCバージョンはBootstrapをベースにしています。

- [ドキュメント](Pure-Theme.md)

- [サンプル](https://github.com/dignite-projects/dignite-abp/tree/main/modules/pure-theme)

### 多テナントの ビュー UI

各テナントは個別のビュー UIを持つことができ、開発者はテナントのUIを簡単にパーソナライズできる。

- [ドキュメント](Views-MultiTenancy.md)

### 多テナント ローカライズ

各テナントは、個別の言語パックを持つことができ、テナントごとにコンテンツを表示することができます。

- [ドキュメント](Localization-MultiTenancy.md)

### BlazoriseUI コンポーネント

Blazoriseをベースにした一連のBlazorコンポーネントが含まれており、ドラッグアンドドロップをサポートするツリーコンポーネント、拡張機能を備えたDataGridなどが含まれています。

- [ドキュメント](BlazoriseUI-Component.md)

### Blazor Ckeditor コンポーネント

Asp.net Blazor向けのCkeditorコンポーネントが提供され、ServerモードとWebAssemblyモードをサポートしています。さらに、動的フォームモジュールとの互換性も提供されています。

- [ドキュメント](Blazor-Ckeditor-Component.md)

## 貢献したいですか?

Dignite ABPは[Abp Framework](https://github.com/abpframework) ビルドオープンソースプロジェクトに基づいています。

このプロジェクトに参加したい方は[貢献の手引き](Contribution/Index.md)を参照してください。
