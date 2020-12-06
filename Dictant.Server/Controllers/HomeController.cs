using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dictant.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dictant.Server.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Dictant.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TasksDbContext _taskDb;
        public HomeController(ILogger<HomeController> logger,TasksDbContext taskDb)
        {
            _taskDb = taskDb;
            _logger = logger;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("Report")]
        public IActionResult Report()
        {
            byte[] fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");


                worksheet.Cells[1, 1].Value = "Количество доступных диктантов";
                worksheet.Cells[2, 1].Value = "Количество диктантво на премодерации";
                worksheet.Cells[3, 1].Value = "Список диктантов";
                
                worksheet.Cells[1, 2].Value = _taskDb.Dictants.Where(x=>x.Approved).Count();
                worksheet.Cells[2, 2].Value = _taskDb.Dictants.Where(x=>!x.Approved).Count();
                var array = _taskDb.Dictants.ToArray();
                for (int i = 0; i < array.Count(); i++)
                {
                    worksheet.Cells[i+3, 2].Value = array[i].Title;
                }
                fileContents = package.GetAsByteArray();
            }

            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "test.xlsx"
            );
        }
    }
}
