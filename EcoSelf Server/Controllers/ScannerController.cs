using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoSelf_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoSelf_Server.Controllers
{
    public class ScannerController : Controller
    {
        private ServerDBContex db;
        public ScannerController(ServerDBContex context)
        {
            db = context;
        }
        public IActionResult GetBarCode(int id)
        {
           return Content(User.Identity.Name);
        }

        public JsonResult SendJson(string BarCode)
        {
            var products = from m in db.Products select m;

            if (!String.IsNullOrEmpty(BarCode))
            {
                products = products.Where(s => s.BarCode.Contains(BarCode));
            }

            return Json(products);
        }

       
    }
}