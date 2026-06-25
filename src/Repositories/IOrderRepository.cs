using TrainingBackend.Entities;

namespace TrainingBackend.Repositories;

public interface IOrderRepository
{
    /// <summary>注文一覧を明細・商品名・クーポン込みで取得する</summary>
    Task<List<Order>> GetAllAsync();

    /// <summary>注文を 1 件、明細・商品名・クーポン込みで取得する</summary>
    Task<Order?> GetByIdAsync(int id);

    Task AddAsync(Order order);

    /// <summary>クーポンコードからクーポンを取得する</summary>
    Task<Coupon?> GetCouponByCodeAsync(string code);

    /// <summary>変更を DB に確定させる（在庫の増減を含む）</summary>
    Task SaveChangesAsync();
}
