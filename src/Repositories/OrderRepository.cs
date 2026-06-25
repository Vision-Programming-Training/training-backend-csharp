using Microsoft.EntityFrameworkCore;
using TrainingBackend.Data;
using TrainingBackend.Entities;

namespace TrainingBackend.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        // Include で明細・商品・クーポンをまとめて読み込み、一覧表示での N+1 を避ける
        return await _db.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.Coupon)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _db.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.Coupon)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
    }

    public async Task<Coupon?> GetCouponByCodeAsync(string code)
    {
        return await _db.Coupons.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
