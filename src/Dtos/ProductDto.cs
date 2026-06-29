namespace TrainingBackend.Dtos;

/// <summary>商品をクライアントに返すための DTO</summary>
/// <param name="Id">商品 ID</param>
/// <param name="Name">商品名</param>
/// <param name="Price">税抜単価</param>
/// <param name="Stock">在庫数</param>
public record ProductDto(int Id, string Name, decimal Price, int Stock);
