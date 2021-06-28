using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Item
    {
        public int Id { get; set; }
        public decimal price { get; set; }
        public int QuantityInStock { get; set; }

        public Product Product { get; set; }
    }
}
