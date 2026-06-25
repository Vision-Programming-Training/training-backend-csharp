using TrainingBackend.Dtos;

namespace TrainingBackend.Services;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetAllAsync();

    Task<OrderDto> GetByIdAsync(int id);

    Task<OrderDto> CreateAsync(CreateOrderRequest request);

    Task<OrderDto> CancelAsync(int id);
}
