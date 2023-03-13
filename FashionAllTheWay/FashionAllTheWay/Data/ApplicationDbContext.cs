using FashionAllTheWay.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FashionAllTheWay.Models.Product;
using FashionAllTheWay.Models.Order;

namespace FashionAllTheWay.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FashionAllTheWay.Models.Product.ProductCreateVM> ProductCreateVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Product.ProductIndexVM> ProductIndexVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Product.ProductEditVM> ProductEditVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Product.ProductDetailsVM> ProductDetailsVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Product.ProductDeleteVM> ProductDeleteVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Order.OrderConfirmVM> OrderConfirmVM { get; set; }
        public DbSet<FashionAllTheWay.Models.Order.OrderIndexVM> OrderIndexVM { get; set; }
    }
}
