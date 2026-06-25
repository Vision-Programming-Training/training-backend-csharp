namespace TrainingBackend.Dtos;

/// <summary>商品をクライアントに返すための DTO</summary>
public record ProductDto(int Id, string Name, decimal Price, int Stock);
