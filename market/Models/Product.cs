using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
