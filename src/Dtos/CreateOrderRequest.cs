using System.ComponentModel.DataAnnotations;

namespace TrainingBackend.Dtos;

/// <summary>注文作成リクエストの 1 明細</summary>
public class CreateOrderItemRequest
{
    /// <summary>注文する商品の ID</summary>
    [Required]
    public int ProductId { get; set; }

    /// <summary>注文数量（1 以上）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "数量は 1 以上で指定してください。")]
    public int Quantity { get; set; }
}

/// <summary>注文作成リクエスト本体</summary>
public class CreateOrderRequest
{
    /// <summary>注文明細の一覧（1 件以上）</summary>
    [Required]
    [MinLength(1, ErrorMessage = "注文には商品を 1 つ以上含めてください。")]
    public List<CreateOrderItemRequest> Items { get; set; } = new();

    /// <summary>クーポンコード（任意）</summary>
    public string? CouponCode { get; set; }
}
