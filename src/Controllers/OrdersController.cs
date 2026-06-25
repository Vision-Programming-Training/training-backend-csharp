using Microsoft.AspNetCore.Mvc;
using TrainingBackend.Dtos;
using TrainingBackend.Services;

namespace TrainingBackend.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>注文一覧を取得する（明細・商品名込み）</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    /// <summary>注文詳細を取得する（存在しない場合は 404）</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        return Ok(order);
    }

    /// <summary>注文を作成する（商品 ID・数量・任意のクーポンコード）</summary>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    /// <summary>注文をキャンセルする（在庫を戻し、ステータスを Cancelled にする）</summary>
    [HttpPost("{id:int}/cancel")]
    public async Task<ActionResult<OrderDto>> Cancel(int id)
    {
        var order = await _orderService.CancelAsync(id);
        return Ok(order);
    }
}
