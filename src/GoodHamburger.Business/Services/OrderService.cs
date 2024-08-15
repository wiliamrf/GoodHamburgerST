using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository, INotifier notifier) : base(notifier)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<bool> Add(Order order)
        {
            var OrderTransition = order;
            
            List<Product> ProductsTransition = new List<Product>();
            foreach (var item in order.Items)
            {
                if (item != null && item.Quantity != 0)
                {
                    ProductsTransition.Add(await _productRepository.SearchId(item.ProductId));
                }
                else
                {
                    Notify("está Faltando a quantidade do produto");
                    return false;

                }
            }
            
            OrderTransition.Items = order.Items.Select(i => new OrderItem
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Product = i.Product,
                //verificacao e correcao do productType
                productType = ProductsTransition.Where(p=>p.Id == i.ProductId).Select(p=>p.productType).FirstOrDefault(),
                OrderId = i.OrderId


            }).ToList();

            var SandwichCount = OrderTransition.Items.Count(P => P.productType == ProductType.Sandwich);
            var DrinkCount = OrderTransition.Items.Count(p => p.productType == ProductType.Drink);
            var PortionCount = OrderTransition.Items.Count(p => p.productType == ProductType.Portion); ;

            var check = CheckErros(SandwichCount, DrinkCount, PortionCount);

            if (check == false) return false;


            var total = CalculateOrder(order, ProductsTransition, SandwichCount, DrinkCount, PortionCount);
            OrderTransition.TotalOrder = total;

            await _orderRepository.Add(OrderTransition);

            return true;
        }


        public async Task<bool> Update(Order order)
        { // Contador de Pordutos
            var OrderTransition = await _orderRepository.SearchIdOrder(order.Id);
            List<Product> ProductsTransition = new List<Product>();
            foreach (var item in order.Items)
            {
                if (item != null && item.Quantity != 0)
                {
                    ProductsTransition.Add(await _productRepository.SearchId(item.ProductId));
                }
                else
                {
                    Notify("está Faltando a quantidade do produto");
                    return false;

                }
            }

            OrderTransition.Items = order.Items.Select(i => new OrderItem
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Product = i.Product,
                //verificacao e correcao do productType
                productType = ProductsTransition.Where(p => p.Id == i.ProductId).Select(p => p.productType).FirstOrDefault(),
                OrderId = i.OrderId


            }).ToList();

            var SandwichCount = OrderTransition.Items.Count(P => P.productType == ProductType.Sandwich);
            var DrinkCount = OrderTransition.Items.Count(p => p.productType == ProductType.Drink);
            var PortionCount = OrderTransition.Items.Count(p => p.productType == ProductType.Portion);


            if (CheckErros(SandwichCount, DrinkCount, PortionCount) == false) return false;



            var total = CalculateOrder(order,ProductsTransition, SandwichCount, DrinkCount, PortionCount);
            OrderTransition.TotalOrder = total;


            await _orderRepository.Update(OrderTransition);
            return true;
        }
        public async Task<bool> Delete(int id)
        {

            var OrderItems = await _orderItemRepository.SearchIdOrderItemlist(id);
            if (OrderItems != null)
            {
                foreach (var item in OrderItems)
                {
                    if (item != null )
                    {
                        await _orderItemRepository.Delete(item.Id);
                    }
                }
            }
            await _orderRepository.Delete(id);

            return true;
        }

        public decimal CalculateOrder(Order order,List<Product> products, int SandwichCount, int DrinkCount, int PortionCount)
        {
            order.TotalOrder = 0;
            foreach (var item in order.Items)
            {
                if (item != null)
                {
                    // decimal Product = _productRepository.GetProductPriceByProductId(item.ProductId);
                    decimal Product = products.Where(p => p.Id == item.ProductId).Sum(p => p.Price);
                    order.TotalOrder += Product;
                };
            }

            if (SandwichCount == 1 && DrinkCount == 1 && PortionCount == 1)
            {

                var total = order.TotalOrder;
                total -= order.TotalOrder * 0.20m;
                return total;
            }
            if (DrinkCount == 1 && SandwichCount == 1)
            {
                var total = order.TotalOrder;
                total -= order.TotalOrder * 0.15m;
                return total;
            }
            if (PortionCount == 1 && SandwichCount == 1)
            {
                var total = order.TotalOrder;
                total -= order.TotalOrder * 0.10m;
                return total;
            }
            return order.TotalOrder;

        }



        public bool CheckErros(int SandwichCount, int DrinkCount, int PortionCount)
        {
            if (SandwichCount <= 0)
            {
                Notify("Não Elaboramos pedidos sem Sandwiches");
                return false;
            }
            if (SandwichCount > 1)
            {
                Notify("Não e possivel escolher dois Sandwiches");
                return false;
            }
            if (DrinkCount > 1)
            {
                Notify("Não e possivel escolher dois Drinks");
                return false;
            }
            if (PortionCount > 1)
            {
                Notify("Não e possivel escolher duas porções");
                return false;
            }

            return true;
        }

       
        
       
        public void Dispose()
        {
            _orderRepository?.Dispose();
            return;
        }

    }
}
