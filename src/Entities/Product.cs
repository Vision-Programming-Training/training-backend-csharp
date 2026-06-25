namespace TrainingBackend.Entities;

/// <summary>商品。名前・税抜単価・在庫数を持つ。</summary>
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>税抜単価。</summary>
    public decimal Price { get; set; }

    /// <summary>在庫数。</summary>
    public int Stock { get; set; }
}
