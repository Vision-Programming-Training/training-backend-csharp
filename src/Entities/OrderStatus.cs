namespace TrainingBackend.Entities;

/// <summary>注文のステータス。</summary>
public enum OrderStatus
{
    /// <summary>未確定（カート相当）。</summary>
    Pending,

    /// <summary>確定済み。</summary>
    Confirmed,

    /// <summary>キャンセル済み。</summary>
    Cancelled
}
