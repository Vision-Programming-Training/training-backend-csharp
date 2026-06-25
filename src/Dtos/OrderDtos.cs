namespace TrainingBackend.Dtos;

/// <summary>注文明細をクライアントに返すための DTO（商品名込み）</summary>
public record OrderItemDto(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);

/// <summary>注文をクライアントに返すための DTO</summary>
public record OrderDto(
    int Id,
    string Status,
    decimal TotalAmount,
    string? CouponCode,
    DateTime CreatedAt,
    IReadOnlyList<OrderItemDto> Items);
