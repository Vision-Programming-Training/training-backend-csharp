using TrainingBackend.Entities;

namespace TrainingBackend.Services;

public interface IPricingService
{
    /// <summary>税抜の小計を求める（単価 × 数量 の合計）</summary>
    decimal CalculateSubtotal(IEnumerable<OrderItem> items);

    /// <summary>税抜小計にクーポン割引を適用する（0 円を下回らない）</summary>
    decimal ApplyCoupon(decimal subtotal, Coupon? coupon);

    /// <summary>小計と送料とクーポンから、税込・円未満四捨五入の合計金額を求める</summary>
    decimal CalculateTotal(decimal subtotal, decimal shippingFee, Coupon? coupon);

    /// <summary>送料を計算する</summary>
    decimal CalculateShippingFee(decimal subtotal);
}
