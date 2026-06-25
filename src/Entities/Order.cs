namespace TrainingBackend.Entities;

/// <summary>注文（明細・ステータス・税込合計を持つ）</summary>
public class Order
{
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    /// <summary>確定時の税込合計</summary>
    public decimal TotalAmount { get; set; }

    public int? CouponId { get; set; }
    public Coupon? Coupon { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
