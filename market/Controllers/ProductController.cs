using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using Microsoft.EntityFrameworkCore;

namespace market.Controllers
{
    public class ProductController : Controller
    {
        private MarketContext _context;
        public ProductController(MarketContext context)
        {
            _context = context;
        }
        [Route("group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id,string name)
        {
            ViewData["GroupName"] = name;
            var products = _context.CategoryToProducts
                .Where(c => c.CategoryId == id)
                .Include(c => c.Product)
                .Select(c => c.Product)
                .ToList();
            return View(products);
        }
    }
}
