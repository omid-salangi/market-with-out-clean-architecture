using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace market.Models
{
    public class AddEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        [Display(Name = "تعداد موجودی")]
        public int QuantityInStock { get; set; }
        [Display(Name = "عکس محصول")]
        public IFormFile Picture { get; set; }

    }
}
