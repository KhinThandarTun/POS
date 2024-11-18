using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Models;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public IActionResult GetProductLists()
        {
            List<Product> products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("by/{id}")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public IActionResult GetProductById(string id)
        {
            var products = _context.Products.FirstOrDefault(x => x.ProductId == id);

            return Ok(products);
        }

        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(new DefaultResponseModel()
            {
                Code = 201,
                Success = true,
                Data = product
            });
        }

    }
}
