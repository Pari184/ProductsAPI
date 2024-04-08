using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productRepo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsRepository productRepo, ILogger<ProductsController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _logger.LogInformation("Fetching all products details...");
                var result = _productRepo.GetAllProducts();
               
                _logger.LogInformation("Products fetched successfully.");
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                return BadRequest(ex);
            }
           
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation("Getting the Product detail..");
                var result = _productRepo.GetProduct(id);

                if (result == null)
                {
                    _logger.LogInformation("Couldn't find the product with this id.");
                    return NotFound();
                }
                _logger.LogInformation("Product fetched successfully.");
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                return BadRequest(ex);
            }
           
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody]Product product)
        {
            // Check for duplication
            try
            {
                var result = new Product();
                if(ModelState.IsValid)
                {
                    result = _productRepo.PostProduct(product);
                                      
                }
                if(result==null)
                {
                    return Conflict("Product with same Name and Brand already exists.");
                }
                _logger.LogInformation("Product saved successfully.");
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the product.");
                return BadRequest(ex);
            }
           
            

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (id != (_productRepo.GetProduct(id)).Id)
                {
                    _logger.LogInformation("Couldn't find the product with this id.");
                    return BadRequest();
                }

                var result = _productRepo.PutProduct(id, product);
                _logger.LogInformation("Product updated successfully.");
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the product.");
                return BadRequest(ex);
            }
            
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = _productRepo.GetProduct(id);
                if (product == null)
                {
                    return Conflict("No product exists with this id to delete.");
                }
                var result = _productRepo.DeleteProduct(id);
                _logger.LogInformation("Product deleted successfully.");
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the product.");
                return BadRequest(ex);
            }
            
        }
    }
}
