using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using market.Data;
using market.Models;

namespace market.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly market.Data.MarketContext _context;

        public IndexModel(market.Data.MarketContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
