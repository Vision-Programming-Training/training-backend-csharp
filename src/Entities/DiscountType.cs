namespace TrainingBackend.Entities;

/// <summary>クーポンの割引種別</summary>
public enum DiscountType
{
    /// <summary>定額割引（円）</summary>
    FixedAmount,

    /// <summary>定率割引（％）</summary>
    Percentage
}
