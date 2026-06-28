using TrainingBackend.Entities;
using TrainingBackend.Services;
using Xunit;

namespace TrainingBackend.Tests;

public class PricingServiceTests
{
    private readonly PricingService _pricing = new();

    private static List<OrderItem> Items(params (decimal price, int qty)[] lines)
    {
        return lines
            .Select(l => new OrderItem { UnitPrice = l.price, Quantity = l.qty })
            .ToList();
    }

    [Fact]
    public void CalculateSubtotal_sums_unit_price_times_quantity()
    {
        var items = Items((300m, 2), (1200m, 1)); // 600 + 1200

        Assert.Equal(1800m, _pricing.CalculateSubtotal(items));
    }

    [Fact]
    public void ApplyCoupon_returns_subtotal_when_coupon_is_null()
    {
        Assert.Equal(1800m, _pricing.ApplyCoupon(1800m, null));
    }

    [Fact]
    public void ApplyCoupon_subtracts_fixed_amount()
    {
        var coupon = new Coupon { DiscountType = DiscountType.FixedAmount, DiscountValue = 500m };

        Assert.Equal(1300m, _pricing.ApplyCoupon(1800m, coupon));
    }

    [Fact]
    public void ApplyCoupon_applies_percentage()
    {
        var coupon = new Coupon { DiscountType = DiscountType.Percentage, DiscountValue = 10m };

        Assert.Equal(1620m, _pricing.ApplyCoupon(1800m, coupon));
    }

    [Fact]
    public void CalculateTotal_adds_10_percent_tax_without_coupon()
    {
        var items = Items((300m, 2), (1200m, 1)); // 小計 1800

        Assert.Equal(1980m, _pricing.CalculateTotal(items, null)); // 1800 * 1.10
    }

    [Fact]
    public void CalculateTotal_applies_coupon_before_tax()
    {
        var items = Items((2500m, 1), (150m, 3)); // 小計 2950
        var coupon = new Coupon { DiscountType = DiscountType.FixedAmount, DiscountValue = 500m };

        Assert.Equal(2695m, _pricing.CalculateTotal(items, coupon)); // (2950 - 500) * 1.10
    }

    [Fact]
    public void CalculateTotal_rounds_to_whole_yen()
    {
        var items = Items((105m, 1)); // 小計 105、税込 115.5 → 四捨五入で 116

        Assert.Equal(116m, _pricing.CalculateTotal(items, null));
    }
}
