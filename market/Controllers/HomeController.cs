using market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using market.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace market.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MarketContext _context; // using dbmodel in view
      


        public HomeController(ILogger<HomeController> logger, MarketContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var product =
                _context.Products.ToList();
            return View(product);
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Item) // we use include for realtion between itself and item for error
                .SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id).
                SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList(); // find all categories for this product

            var vm = new DetailViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(vm);
        }
        [Authorize]
        public IActionResult AddToCart(int ItemId)
        {
            var product = _context.Products.Include(p=>p.Item).SingleOrDefault(p => p.ItemId == ItemId);
            if (product != null)
            {
                int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Orders.FirstOrDefault(o => o.UserId == userid && !o.IsFinally);
                if (order != null)
                {
                    var orderDetail =
                        _context.OrderDetails.FirstOrDefault(d =>
                            d.OrderId == order.OrderId && d.ProductId == product.Id);
                    if (orderDetail !=null)
                    {
                        orderDetail.count += 1;
                    }
                    else
                    {
                        _context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.price,
                            count = 1
                        });
                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFinally = false,
                        CreateDate = DateTime.Now,
                        UserId = userid
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.Id,
                        Price = product.Item.price,
                        count=1
                    });

                }

                _context.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }
        [Authorize]
        public IActionResult ShowCart()
        {
            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.Where(p => p.UserId == userid && !p.IsFinally).Include(o => o.OrderDetails)
                .ThenInclude(c => c.Product).FirstOrDefault();
            return View(order);
        }
        [Authorize]
        public IActionResult RemoveCart(int DetailId)
        {
            var orderDetail = _context.OrderDetails.Find(DetailId); // it will know from itself
            _context.Remove(orderDetail);
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
