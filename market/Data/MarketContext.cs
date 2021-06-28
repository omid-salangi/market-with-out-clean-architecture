using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using market.Models;
using Microsoft.EntityFrameworkCore;

namespace market.Data
{
    public class MarketContext : DbContext

    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; } // tables in models
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        #region inital data
        protected override void OnModelCreating(ModelBuilder modelBuilder) // for initial data of a table
        {
            #region carttocategory primary key

            modelBuilder.Entity<CategoryToProduct>().HasKey(p => new { p.ProductId, p.CategoryId });
            //modelBuilder.Entity<CategoryToProduct>().HasOne(pt => pt.Category).WithMany(p => p.CategoryToProducts)
            //    .HasForeignKey(pt => pt.CategoryId);
            //modelBuilder.Entity<CategoryToProduct>().HasOne(pt => pt.Product).WithMany(p => p.CategoryToProducts)
            //    .HasForeignKey(pt => pt.ProductId);



            #endregion
            #region category
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "موبایل ",
                    Description = "تلفن همراه "
                },
                new Category()
                {
                    Id = 2,
                    Name = "لپ تاپ",
                    Description = "کامپیوتر شخصی"
                },
                new Category()
                {
                    Id = 3,
                    Name = "تبلت",
                    Description = "رایانک مالشی"
                },
                new Category()
                {
                    Id = 4,
                    Name = "ساعت",
                    Description = "ساعت مچی"
                }
            );
            #endregion

            #region realize relations betwwen tables



            #endregion

            #region change price to money in table in other way of fluent api

            modelBuilder.Entity<Item>().Property(p => p.price).HasColumnType("Money");
            modelBuilder.Entity<Item>().HasKey(w => w.Id);

            #endregion

            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    price = 40.0M,
                    QuantityInStock = 5
                },
                new Item()
                {
                    Id = 2,
                    price = 30.0M,
                    QuantityInStock = 8
                },
                new Item()
                {
                    Id = 3,
                    price = 25.0M,
                    QuantityInStock = 3
                }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Description = "یکی از بهترین محصولات حال حاضر می باشد.",
                    ItemId = 1,
                    Name = "اکسپریا 1 iii"
                },
                new Product()
                {
                    Id = 2,
                    Description = "تلفن همراه سامسونگ",
                    ItemId = 2,
                    Name = "گلکسی s21"
                },
                new Product()
                {
                    Id = 3,
                    Description = "تلفن همراه شیائومی ",
                    ItemId = 3,
                    Name = "می 11 الترا"
                }
                );

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 3 }
                );
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
