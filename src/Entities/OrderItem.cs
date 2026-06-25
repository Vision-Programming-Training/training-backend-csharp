namespace TrainingBackend.Entities;

/// <summary>注文明細（どの商品を何個注文したかを表す）</summary>
public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    /// <summary>注文時点の税抜単価（スナップショット）</summary>
    public decimal UnitPrice { get; set; }
}
