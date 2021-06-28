using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using market.Data.Repositories;
using market.Models;

namespace market.Services
{
    public class GroupRepositories : IGroupRepositories
    {
        private MarketContext _context;

        public GroupRepositories(MarketContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGrouptForShow()
        {
            return _context.Categories.Select(c => new ShowGroupViewModel()
            {
                GroupId = c.Id,
                Name = c.Name,
                ProductCount = _context.CategoryToProducts.Count(g => g.CategoryId == c.Id)
            }).ToList();
        }

     
    }
}
