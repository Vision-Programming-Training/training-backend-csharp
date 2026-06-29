namespace TrainingBackend.Dtos;

/// <summary>クーポンをクライアントに返すための DTO</summary>
/// <param name="Code">クーポンコード</param>
/// <param name="DiscountType">割引種別（FixedAmount / Percentage）</param>
/// <param name="DiscountValue">割引値（FixedAmount なら円、Percentage なら％）</param>
public record CouponDto(
    string Code,
    string DiscountType,
    decimal DiscountValue);
