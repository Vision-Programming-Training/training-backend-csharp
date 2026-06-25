using Microsoft.AspNetCore.Mvc;
using TrainingBackend.Dtos;
using TrainingBackend.Exceptions;
using TrainingBackend.Repositories;

namespace TrainingBackend.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>商品一覧を取得する。</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        var dtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock));
        return Ok(dtos);
    }

    /// <summary>商品詳細を取得する。存在しない場合は 404。</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"商品が見つかりません (ProductId: {id})");

        return Ok(new ProductDto(product.Id, product.Name, product.Price, product.Stock));
    }
}
