namespace TrainingBackend.Dtos;

/// <summary>注文明細をクライアントに返すための DTO（商品名込み）</summary>
/// <param name="ProductId">商品 ID</param>
/// <param name="ProductName">商品名</param>
/// <param name="Quantity">注文数量</param>
/// <param name="UnitPrice">注文時点の税抜単価（スナップショット）</param>
/// <param name="LineTotal">この明細の小計（税抜単価 × 数量）</param>
public record OrderItemDto(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);

/// <summary>注文をクライアントに返すための DTO</summary>
/// <param name="Id">注文 ID</param>
/// <param name="Status">注文のステータス（Pending / Confirmed / Cancelled）</param>
/// <param name="TotalAmount">確定時の税込合計</param>
/// <param name="CouponCode">適用したクーポンコード（未適用なら null）</param>
/// <param name="CreatedAt">注文作成日時</param>
/// <param name="Items">注文明細の一覧</param>
public record OrderDto(
    int Id,
    string Status,
    decimal TotalAmount,
    string? CouponCode,
    DateTime CreatedAt,
    IReadOnlyList<OrderItemDto> Items);
