using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Models;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public IActionResult GetProductLists()
        {
            List<Supplier> suppliers = _context.Suppliers.ToList();
            return Ok(suppliers);
        }

        [HttpPost("create")]
        public IActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return Ok(new DefaultResponseModel()
            {
                Code = 201,
                Success = true,
                Data = supplier
            });
        }
    }
}
