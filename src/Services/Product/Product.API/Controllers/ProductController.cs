using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.GenericResponse;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GenericResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById([FromRoute] string id)
        {
            var product = await _service.GetProduct(id);
            if (product == null)
            {
                _logger.LogInformation($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet(("GetAllProducts"))]
        [ProducesResponseType(typeof(IEnumerable<GenericResponse<ProductResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
           
            var products = await _service.GetProducts();
            _logger.LogInformation($"Products not found.");
            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]  CreateProductDto productDto)
        {
           var response = await _service.CreateProduct(productDto);

            return CreatedAtRoute("GetProduct", response.Data, productDto);
        }
        /* [Route("[action]/{category}", Name = "GetProductByCategory")]
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
         public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
         {
             var products = await _service.GetProductByCategory(category);
             return Ok(products);
         }

         [HttpPost]
         [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
         public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
         {
             await _service.CreateProduct(product);

             return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
         }

         [HttpPut]
         [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
         public async Task<IActionResult> UpdateProduct([FromBody] Product product)
         {
             return Ok(await _service.UpdateProduct(product));
         }

         [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
         [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
         public async Task<IActionResult> DeleteProductById(long id)
         {
             return Ok(await _service.DeleteProduct(id));
         }*/
    }
}
