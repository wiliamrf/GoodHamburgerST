using GoodHamburger.Business.Models;
using GoodHamburger.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Services
{
    public class SeedService
    {
        private readonly GHContext _GHContext;

        public SeedService(GHContext context)
        {
            _GHContext = context;
        }

        //public void Seeding(GHContext context)
        public void Seed()
        {
            if (_GHContext.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
            new Product { Name = "X Burger", Price = 5.00m, productType = ProductType.Sandwich },
            new Product { Name = "X Egg", Price = 4.50m, productType = ProductType.Sandwich },
            new Product { Name = "X Bacon", Price = 7.00m, productType = ProductType.Sandwich },
            new Product { Name = "Fries", Price = 2.00m, productType = ProductType.Portion },
            new Product { Name = "Soft Drink", Price = 2.50m, productType = ProductType.Drink }

            };
            _GHContext.Products.AddRange(products);
            _GHContext.SaveChanges();

            var orders = new Order[]
        {
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[0].Id, Quantity = 1, productType = ProductType.Sandwich },
                    new OrderItem { ProductId = products[3].Id, Quantity = 1, productType = ProductType.Portion  },
                    new OrderItem { ProductId = products[4].Id, Quantity = 1 ,productType = ProductType.Drink}
                },
                TotalOrder = 7.20m // Preço com desconto
            },
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[1].Id, Quantity = 1, productType = ProductType.Sandwich  },
                    new OrderItem { ProductId = products[4].Id, Quantity = 1, productType = ProductType.Drink }
                },
                TotalOrder = 5.95m // Preço com desconto
            },
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[2].Id, Quantity = 1, productType = ProductType.Sandwich },
                    new OrderItem { ProductId = products[3].Id, Quantity = 1, productType = ProductType.Portion }
                },
                TotalOrder = 8.10m // Preço com desconto
            },
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[2].Id, Quantity = 1, productType = ProductType.Sandwich },
                    new OrderItem { ProductId = products[4].Id, Quantity = 1, productType = ProductType.Drink }
                },
                TotalOrder = 7.00m // Preço sem desconto
            },
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[1].Id, Quantity = 1, productType = ProductType.Sandwich },
                    new OrderItem { ProductId = products[3].Id, Quantity = 1, productType = ProductType.Portion }
                },
                TotalOrder = 6.30m // Preço com desconto
            },
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[0].Id, Quantity = 1, productType = ProductType.Sandwich },
                    new OrderItem { ProductId = products[4].Id, Quantity = 1, productType = ProductType.Drink }
                },
                TotalOrder = 6.75m // Preço com desconto
            }
        };

            _GHContext.Orders.AddRange(orders);
            _GHContext.SaveChanges();



        }
    }
}
