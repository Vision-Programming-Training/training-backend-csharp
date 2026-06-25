using System.ComponentModel.DataAnnotations;

namespace TrainingBackend.Dtos;

/// <summary>注文作成リクエストの 1 明細</summary>
public class CreateOrderItemRequest
{
    [Required]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "数量は 1 以上で指定してください。")]
    public int Quantity { get; set; }
}

/// <summary>注文作成リクエスト本体</summary>
public class CreateOrderRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "注文には商品を 1 つ以上含めてください。")]
    public List<CreateOrderItemRequest> Items { get; set; } = new();

    /// <summary>クーポンコード（任意）</summary>
    public string? CouponCode { get; set; }
}
