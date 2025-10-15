using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private static List<Products> products = new List<Products>()
        {
            new Products { Id = 1, Name = "Laptop", Category = "Electronics", Price = 75000 },
            new Products { Id = 2, Name = "Headphones", Category = "Electronics", Price = 2500 },
            new Products { Id = 3, Name = "Coffee Mug", Category = "Kitchen", Price = 300 }
        };
        //GET:api/Product
        [HttpGet("GetProducts")]
        public IEnumerable<Products> GetProducts()
        {
            return products;
        }
        // POST: api/Product
        [HttpPost]
        public ActionResult<Products> CreateProduct([FromBody] Products newProduct)
        {
            newProduct.Id = products.Count + 1; 
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id }, newProduct);
        }

        // PATCH: api/Product/1
        [HttpPatch("{id}")]
        public ActionResult<Products> UpdateProduct(int id, [FromBody] Products updatedProduct)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null) return NotFound();

            // Update only fields that are not null
            if (!string.IsNullOrEmpty(updatedProduct.Name)) product.Name = updatedProduct.Name;
            if (!string.IsNullOrEmpty(updatedProduct.Category)) product.Category = updatedProduct.Category;
            if (updatedProduct.Price != 0) product.Price = updatedProduct.Price;

            return product;
        }

        // DELETE: api/Product/1
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null) return NotFound();

            products.Remove(product);
            return NoContent();
        }
    }
}
