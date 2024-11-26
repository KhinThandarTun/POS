using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Models;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> ExcelImport(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("File not Found!.");
            }

            using (var stream = file.OpenReadStream())
            {
                var purchases = await ReadExcelFile<Purchase>(stream);
                foreach(var purchase in purchases)
                {
                    _context.Purchases.Add(purchase);
                }

                await _context.SaveChangesAsync();
            }

            return Ok("Purchase imported successfully.");
        }

        private async Task<List<Purchase>> ReadExcelFile<T>(Stream stream) where T : new()
        {
            List<Purchase> purchases = new List<Purchase>();
            using (var workbook = new XLWorkbook(stream))
            {
                 var worksheet = workbook.Worksheets.First(); 
                 var rows = worksheet.RowsUsed().Skip(1);
                foreach (var row in rows) 
                {
                    Purchase purchase = new Purchase
                    {
                        PurchaseVno = row.Cell(1).GetString(),
                        SupplierId = row.Cell(2).GetString(),
                        TotalAmt = row.Cell(3).GetDouble()
                    };

                    purchases.Add(purchase);
                    
                }
            }
            return purchases;
        }
    }
}
