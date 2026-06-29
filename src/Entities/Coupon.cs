namespace TrainingBackend.Entities;

/// <summary>クーポン（割引ルールを表す）</summary>
public class Coupon
{
    /// <summary>主キー</summary>
    public int Id { get; set; }

    /// <summary>クーポンコード（利用者が入力する文字列）</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>割引種別（定額／定率）</summary>
    public DiscountType DiscountType { get; set; }

    /// <summary>割引値（FixedAmount なら円、Percentage なら％）</summary>
    public decimal DiscountValue { get; set; }
}
