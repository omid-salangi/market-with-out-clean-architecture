using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace market.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MarketContext _context;
        public IEnumerable<Product> Products { get; set; }

        public IndexModel(MarketContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Products = _context.Products.
                Include(p=>p.Item);

        }

        public void OnPost()
        {
        }
    }
}
