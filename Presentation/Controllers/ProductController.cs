using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DatatoObject_Dtos_.ProductDtos;
using Shared.PaginatedModel;
using Shared.QueryModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public ProductController(IServiceManager _service)
        {
            serviceManager = _service;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralPaginatedModel<ProductDto>>> GetAllProducts([FromQuery]ProductQueryData productQueryData)
        {
            //throw new NotImplementedException("This method is not implemented yet. Please implement it before using it.");
            var products = await serviceManager.ProductService.GetAllProductsAsync(productQueryData); // Consider using async/await properly
            if (products == null || !products.Items.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(Id); // Consider using async/await properly
            if (product == null)
            {
                return NotFound("No products found.");
            }
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await serviceManager.ProductService.GetAllProductBrandsAsync(); // Consider using async/await properly
            if (Brands == null)
            {
                return NotFound("No products found.");
            }
            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetAllTypes()
        {
            var Types = await serviceManager.ProductService.GetAllProductTypesAsync(); // Consider using async/await properly
            if (Types == null)
            {
                return NotFound("No products found.");
            }
            return Ok(Types);
        }
    }
}
