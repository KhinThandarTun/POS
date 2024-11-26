using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.API.Models;
using System.IO;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("import")]
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


        [HttpGet("export")]
        public async Task<IActionResult> GenerateExcelFile()
        {
            var purchases = await _context.Purchases.ToListAsync();
            using (var workbook = new XLWorkbook()) 
            {
                var worksheet = workbook.AddWorksheet("Purchase");

                //Add header row
                worksheet.Row(1).Cell(1).Value = "PurchaseVno";
                worksheet.Row(1).Cell(2).Value = "SupplierId";
                worksheet.Row(1).Cell(3).Value = "TotalAmt";

                //Add Rows
                for (int i = 0; i < purchases.Count; i++)
                { 
                    worksheet.Cell(i + 2, 1).Value = purchases[i].PurchaseVno;
                    worksheet.Cell(i + 2, 2).Value = purchases[i].SupplierId;
                    worksheet.Cell(i + 2, 3).Value = purchases[i].TotalAmt;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                        "Purchase.xlsx");
                }
            }
        }
    }
}
