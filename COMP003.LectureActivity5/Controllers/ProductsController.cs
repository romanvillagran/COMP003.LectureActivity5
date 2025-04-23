using COMP003.LectureActivity5.Data;
using COMP003.LectureActivity5.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP003.LectureActivity5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private object p;
        private object product;
        private object existingProdcut;
        private object updatedproduct;

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(ProductStore.Products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
        var ActionResult = ProductStore.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = ProductStore.Products.Max(p => p.Id) + 1;

            ProductStore.Products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProdcut(int id, Product updateProduct)
        {
            var existingProduct = ProductStore.Products.FirstOrDefault(p => p.Id == id);

            if (existingProdcut is null)
                return NotFound();

            existingProduct.Name = updatedproduct.Name;
            existingProduct.Price = updatedproduct.Price;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProdcut(int id)
        {
            var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);

            if (product is not null)
            {
                ProductStore.Products.Remove(product);

                return NoContent();
            }

            return NotFound();
        }
        [HttpGet("filter")]
        public ActionResult<List<Product>> FilterProdcut(decimal price)
        {
            var filteredProducts = ProductStore.Products
                .Where(p => p.Price <= price)
                .OrderBy(p => p.Price)
                .ToList();

            return Ok(filteredProducts);
        }
        [HttpGet("names")]
        public ActionResult<List<string>> GetProductNames()
        {
            var productNames = ProductStore.Products
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();

            return Ok(productNames);
        }
    }
}
