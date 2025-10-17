using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GET:api/Product/GetProducts
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
                return NotFound("No products found.");
            return Ok(products);
        }

        //POST:api/Product
        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] Products newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Invalid Product");
            }
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(newProduct);
        }

        //PATCH:api/updateProduct
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Products updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            if (!string.IsNullOrEmpty(updatedProduct.Name))
                product.Name = updatedProduct.Name;
            if (!string.IsNullOrEmpty(updatedProduct.Category))
                product.Category = updatedProduct.Category;
            if (updatedProduct.Price != 0)
                product.Price = updatedProduct.Price;
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        //DELETE:api/DeleteProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            var product= await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

    }
}
