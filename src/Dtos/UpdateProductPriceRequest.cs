using System.ComponentModel.DataAnnotations;

namespace TrainingBackend.Dtos;

/// <summary>商品価格の変更リクエスト</summary>
public class UpdateProductPriceRequest
{
    /// <summary>変更後の税抜単価（0 以上）</summary>
    [Range(0, double.MaxValue, ErrorMessage = "価格は 0 以上で指定してください。")]
    public decimal Price { get; set; }
}
