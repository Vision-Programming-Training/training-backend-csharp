using TrainingBackend.Entities;

namespace TrainingBackend.Data;

/// <summary>
/// 初期データを投入する。アプリ起動時に一度だけ呼ばれる。
/// 既にデータがあれば何もしない（再起動で二重投入しない）。
/// </summary>
public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Products.Any())
        {
            return;
        }

        // --- 商品（在庫 0 の商品を 1 件含める） ---
        var notebook = new Product { Name = "ノート", Price = 300m, Stock = 50 };
        var pen = new Product { Name = "ボールペン", Price = 150m, Stock = 100 };
        var mug = new Product { Name = "マグカップ", Price = 1200m, Stock = 20 };
        var tshirt = new Product { Name = "Tシャツ", Price = 2500m, Stock = 10 };
        var sticker = new Product { Name = "ステッカー", Price = 200m, Stock = 0 }; // 在庫切れ
        var toteBag = new Product { Name = "トートバッグ", Price = 1800m, Stock = 15 };
        var keychain = new Product { Name = "キーホルダー", Price = 600m, Stock = 30 };

        db.Products.AddRange(notebook, pen, mug, tshirt, sticker, toteBag, keychain);

        // --- クーポン（定額・定率を各 1） ---
        var welcomeCoupon = new Coupon
        {
            Code = "WELCOME500",
            DiscountType = DiscountType.FixedAmount,
            DiscountValue = 500m
        };
        var saleCoupon = new Coupon
        {
            Code = "SALE10",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 10m
        };

        db.Coupons.AddRange(welcomeCoupon, saleCoupon);

        // 商品・クーポンの Id を確定させてから注文を作る。
        db.SaveChanges();

        // --- 既存の注文（一覧・明細の確認用） ---
        // 合計金額は「(税抜小計 - 割引) × 1.10」を円未満四捨五入した値。
        var orderA = new Order
        {
            Status = OrderStatus.Confirmed,
            CreatedAt = new DateTime(2026, 1, 10, 10, 0, 0, DateTimeKind.Utc),
            Items =
            {
                new OrderItem { ProductId = notebook.Id, Quantity = 2, UnitPrice = notebook.Price },
                new OrderItem { ProductId = mug.Id, Quantity = 1, UnitPrice = mug.Price }
            },
            TotalAmount = 1980m // (600 + 1200) * 1.10
        };

        var orderB = new Order
        {
            Status = OrderStatus.Confirmed,
            CouponId = welcomeCoupon.Id,
            CreatedAt = new DateTime(2026, 1, 12, 14, 30, 0, DateTimeKind.Utc),
            Items =
            {
                new OrderItem { ProductId = tshirt.Id, Quantity = 1, UnitPrice = tshirt.Price },
                new OrderItem { ProductId = pen.Id, Quantity = 3, UnitPrice = pen.Price }
            },
            TotalAmount = 2695m // (2950 - 500) * 1.10
        };

        var orderC = new Order
        {
            Status = OrderStatus.Confirmed,
            CouponId = saleCoupon.Id,
            CreatedAt = new DateTime(2026, 1, 15, 9, 15, 0, DateTimeKind.Utc),
            Items =
            {
                new OrderItem { ProductId = toteBag.Id, Quantity = 1, UnitPrice = toteBag.Price }
            },
            TotalAmount = 1782m // (1800 - 10%) * 1.10
        };

        db.Orders.AddRange(orderA, orderB, orderC);
        db.SaveChanges();
    }
}
