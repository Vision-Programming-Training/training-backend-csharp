using TrainingBackend.Entities;

namespace TrainingBackend.Services;

/// <summary>
/// 金額計算を担当する（小計・クーポン割引・税込合計・送料の 4 ステップ）
/// 計算の流れ: 税抜小計 → クーポン割引 → 消費税を乗せて円未満四捨五入 → 送料（非課税）を加算
/// 送料はクーポン適用前の税抜小計で判定する
/// </summary>
public class PricingService : IPricingService
{
    /// <summary>消費税率（10%）</summary>
    public const decimal TaxRate = 0.10m;

    private const decimal ShippingFee = 500m;

    private const decimal ShippingFeeBorder = 5000m;

    public decimal CalculateSubtotal(IEnumerable<OrderItem> items)
    {
        return items.Sum(i => i.UnitPrice * i.Quantity);
    }

    public decimal ApplyCoupon(decimal subtotal, Coupon? coupon)
    {
        if (coupon is null)
        {
            return subtotal;
        }

        var discounted = coupon.DiscountType switch
        {
            DiscountType.FixedAmount => subtotal - coupon.DiscountValue,
            DiscountType.Percentage => subtotal * (1m - coupon.DiscountValue / 100m),
            _ => subtotal
        };

        // 割引で 0 円未満になっても請求は 0 円までとする
        return discounted < 0m ? 0m : discounted;
    }

    public decimal CalculateShippingFee(decimal subtotal)
    {
        return subtotal >= ShippingFeeBorder
            ? 0m
            : ShippingFee;
    }

    public decimal CalculateTotal(decimal subtotal, decimal shippingFee, Coupon? coupon)
    {
        var discounted = ApplyCoupon(subtotal, coupon);
        var withTax = discounted * (1m + TaxRate);

        // 円未満は四捨五入（0.5 は切り上げ）
        return Math.Round(withTax, 0, MidpointRounding.AwayFromZero) + shippingFee;
    }
}
