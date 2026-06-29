namespace TrainingBackend.Entities;

/// <summary>注文明細（どの商品を何個注文したかを表す）</summary>
public class OrderItem
{
    /// <summary>主キー</summary>
    public int Id { get; set; }

    /// <summary>所属する注文の外部キー</summary>
    public int OrderId { get; set; }

    /// <summary>所属する注文（ナビゲーションプロパティ）</summary>
    public Order Order { get; set; } = null!;

    /// <summary>対象商品の外部キー</summary>
    public int ProductId { get; set; }

    /// <summary>対象商品（ナビゲーションプロパティ）</summary>
    public Product Product { get; set; } = null!;

    /// <summary>注文数量</summary>
    public int Quantity { get; set; }

    /// <summary>注文時点の税抜単価(スナップショット)</summary>
    public decimal UnitPrice { get; set; }
}
