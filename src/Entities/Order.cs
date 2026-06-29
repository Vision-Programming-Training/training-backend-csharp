namespace TrainingBackend.Entities;

/// <summary>注文（明細・ステータス・税込合計を持つ）</summary>
public class Order
{
    /// <summary>主キー</summary>
    public int Id { get; set; }

    /// <summary>注文のステータス（未確定／確定済み／キャンセル済み）</summary>
    public OrderStatus Status { get; set; }

    /// <summary>確定時の税込合計</summary>
    public decimal TotalAmount { get; set; }

    /// <summary>適用したクーポンの外部キー（未適用なら null）</summary>
    public int? CouponId { get; set; }

    /// <summary>適用したクーポン（ナビゲーションプロパティ）</summary>
    public Coupon? Coupon { get; set; }

    /// <summary>注文作成日時</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>注文明細の一覧</summary>
    public List<OrderItem> Items { get; set; } = new();
}
