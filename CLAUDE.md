# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

新人研修用の教材サーバー。ミニ EC の注文 API を ASP.NET Core (.NET 8) / EF Core (SQLite) で実装したもの。`main` は「正しく動く完成形」で、課題ごとに別ブランチが用意される。受講者は「既存コードを読んで・探して・直す」練習をする。フロントは別リポジトリ（training-frontend）。

ドキュメント類はすべて日本語。コミットメッセージ・PR・コメントも日本語が基本。

## Commands

すべてリポジトリルートで実行する。

```bash
dotnet run --project src    # ビルド〜DB初期化〜起動まで自動。Swagger UI: http://localhost:5000/swagger
dotnet build                # ビルドのみ
dotnet test                 # 全テスト（xUnit）
dotnet restore              # 依存復元
```

単一テストの実行:

```bash
dotnet test --filter "FullyQualifiedName~OrderServiceTests"
dotnet test --filter "Name=CreateAsync_creates_order_decrements_stock_and_computes_total"
```

DB リセット: `src/training.db`（および `-shm` / `-wal`）を削除して再度 `dotnet run --project src`。起動時に `EnsureCreated` + seed で初期データから作り直される（マイグレーションは使っていない）。

CI は PR / push (main) で `dotnet restore` → `dotnet build --configuration Release` → `dotnet test` を回す。**赤い状態でコミット・PR しない**のが研修ルール。

## Architecture

3 層構成（Controller / Service / Repository）。「直したい挙動はどの層にあるか」を探させるため、層の責務を明確に分けている。

- **Controllers** (`src/Controllers/`) — 入口。意図的に薄く、ロジックを書かない。例外処理も書かない（下記ミドルウェアに任せる）。
- **Services** (`src/Services/`) — 業務ロジックの中心。**修正系課題の主戦場。** `OrderService`（在庫チェック・クーポン適用・注文作成/キャンセル・在庫戻し）と `PricingService`（金額計算）。
- **Repositories** (`src/Repositories/`) — EF Core 経由の DB アクセスのみ。
- `Entities/` テーブル＝クラス、`Dtos/` API 入出力（record 型）、`Data/` `AppDbContext` と `SeedData`、`Middleware/`、`Exceptions/`、`Program.cs`（起動・DI・CORS・DB初期化）。

各層は interface 経由で DI 登録される（`Program.cs` で `AddScoped`）。新しい Service / Repository を足すときは interface とセットで登録する。

### エラー処理の規約

Service 層は **`NotFoundException` (→ HTTP 404) / `BusinessRuleException` (→ HTTP 400)** を投げる（`src/Exceptions/AppExceptions.cs`）。`ExceptionHandlingMiddleware` がこれらを捕まえて `{ status, error }` の JSON に変換する。それ以外の例外は 500。**Controller で try/catch を書かず、Service で適切な例外を投げる**のがこのコードの流儀。

### 金額計算の規約

`PricingService` は「税抜小計 → クーポン割引 → 消費税 (`TaxRate = 0.10`) を乗せて円未満四捨五入 (`MidpointRounding.AwayFromZero`)」の順。割引で 0 円未満になっても 0 円までにクランプ。注文時の単価は `OrderItem.UnitPrice` にスナップショット保存されるため、後から商品価格を変えても既存注文の合計は変わらない。

## Tests

`tests/` の xUnit。各テストは `UseInMemoryDatabase` で独立したインメモリ DB を立て、実 DB / SQLite には触れない（`OrderServiceTests.CreateContext` 参照）。Service を本物の Repository + `PricingService` と組み合わせて検証する結合寄りのスタイル。`main` ではすべてグリーンで、これが「正しい挙動の保証」。コードを直したら必ず `dotnet test`。

## Conventions (from CONTRIBUTING.md)

- `main` に直接コミットしない。ブランチを切る: `feature/` `fix/` `refactor/` `docs/`。
- 1 コミット = 1 つの意味のある変更。ビルドが通らない/テストが赤い状態でコミットしない。
- PR は `.github/pull_request_template.md` の項目（何を変えたか / どう確認したか / 影響範囲）を埋める。
