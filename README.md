
<p align="center"><h1 align="center">WordReviser</h1></p>

<p align="center">Blazor Serverベースのword校正Webアプリケーション</p>

<p align="center"><a href="./LICENSE"><img src="https://img.shields.io/github/license/icchon/WordReviser" alt="license"></a></p>

<br>

## Contents

- [Overview](#overview)

- [Features](#features)

- [Technology Stack](#technology-stack)

- [Getting Started](#getting-started)

- [License](#license)

---

## Overview

`WordReviser`は、Blazor Serverと.NET 8で構築された文章校正Webアプリケーションである。アップロードされたWord (.docx) ファイルの内容をLLM（大規模言語モデル）を利用して校正し、変更点を差分表示する。

---

## Features

- **.docxファイルのアップロード:** 校正したいWordファイルをWeb UIからアップロードする。

- **自動校正:** アップロードされたファイルは自動的にテキストに変換され、LLMによって校正される。

- **差分表示:** `DiffMatchPatch`ライブラリを使用し、校正前後の文章の差分をハイライト表示する。

- **ファイル変換:** `Pandoc`ライブラリを利用して`.docx`からHTMLへの変換を行う。

- **PDFプレビュー:** `Syncfusion PDF Viewer`コンポーネントでドキュメントのプレビューを表示する。

- **ダウンロード:** 校正後の文章を`.docx`ファイルとしてダウンロードする。

---

## Technology Stack

- **Backend & Frontend:** Blazor Server (.NET 8)

- **UI Components:** MudBlazor, Blazor Bootstrap, Syncfusion Blazor

- **Document Processing:** Pandoc, Aspose.HTML, Select.HtmlToPdf

- **Diff Engine:** DiffMatchPatch

---

## Getting Started

### Prerequisites

- .NET 8.0 SDK

### Installation

```sh

❯ git clone https://github.com/icchon/WordReviser
❯ cd WordReviser
❯ dotnet restore
```

### Usage

```sh

❯ dotnet run
```

アプリケーションを起動し、Webブラウザで指定されたURL（例: `https://localhost:7226`）にアクセスする。

---

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

© 2025 icchon
