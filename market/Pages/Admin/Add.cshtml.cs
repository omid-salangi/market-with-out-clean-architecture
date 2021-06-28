using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace market.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MarketContext _context;
        [BindProperty] // for retuen and come to this property
        public AddEditViewModel Product { get; set; }

        public AddModel(MarketContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }       
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = new Item()
            {
                price = Product.Price,
                QuantityInStock = Product.QuantityInStock
            };
            _context.Add(item);
            _context.SaveChanges();

            var pro = new Product()
            {
                Name = Product.Name,
                Item = item,
                Description = Product.Description

            };
            _context.Add(pro);
            _context.SaveChanges();
            pro.ItemId = item.Id; // important
            _context.SaveChanges();

            if (Product.Picture?.Length >0)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot", "img", pro.Id + Path.GetExtension(Product.Picture.FileName));
                using (var stream = new FileStream(filepath,FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }
            return RedirectToPage("Index");
        }
    }
}
