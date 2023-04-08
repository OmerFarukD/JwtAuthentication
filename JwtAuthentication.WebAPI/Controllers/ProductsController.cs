using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;
using JwtAuthentication.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IServiceGeneric<Product, ProductDto> _productService;

    public ProductsController(IServiceGeneric<Product, ProductDto> productService)
    {
        _productService = productService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        return ActionResultInstance(await _productService.GetAllAsync());
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        return ActionResultInstance(await _productService.AddAsync(productDto));
    }

    [HttpPost("delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute(Name = "id")]int id)
    {
        return ActionResultInstance(await _productService.Remove(id));
    }
    
}