using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace market.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MarketContext _context;

        public EditModel(MarketContext context)
        {
            _context = context;
        }
        [BindProperty] // for retuen and come to this property
        public AddEditViewModel Product { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products
                .Include(p => p.Item)
                .Where(p => p.Id == id)
                .Select(s => new AddEditViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                QuantityInStock = s.Item.QuantityInStock,
                Price = s.Item.price
            }).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(p => p.Id == product.ItemId);

            product.Name = Product.Name;
            product.Description = Product.Description;
            item.price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;
            if (Product.Picture?.Length > 0)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot", "img", product.Id + Path.GetExtension(Product.Picture.FileName));
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            _context.SaveChanges();
            return RedirectToPage("index");
        }
    }
}
