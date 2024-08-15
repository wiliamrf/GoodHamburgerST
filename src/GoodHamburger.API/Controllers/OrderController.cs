using AutoMapper;
using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Business.Notifications;
using GoodHamburger.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoodHamburger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : MainController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IOrderService orderService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            return CustomResponse(orders); ;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderbyId(int id)
        {
            var orders = await _orderRepository.SearchIdOrder(id);
            if (orders == null)
            {
                NotifyError("Ordem não cadastrada");
                return CustomResponse();
            }

            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO orderDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (orderDTO == null) return NotFound();


            await _orderService.Add(_mapper.Map<Order>(orderDTO));
            var Orderend = await _orderRepository.SearchIdEnd();

            return CustomResponse(Orderend);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                NotifyError("Os Ids informados não são iguais");
                return CustomResponse();
            }
            if (!ModelState.IsValid) return CustomResponse();

            if (await _orderRepository.SearchIdOrder(id) == null)
            {
                NotifyError("Pedido não existe");
                return CustomResponse();
            }


            await _orderService.Update(_mapper.Map<Order>(orderDTO));
            var OrderEnd = _orderRepository.SearchIdOrder(id);
            
            return CustomResponse(OrderEnd);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderDTO>> DeleteOrder(int id)
        {
            var OrderDTO = await _orderRepository.SearchIdOrder(id);

            if (OrderDTO == null) return NotFound();
            
            await _orderService.Delete(id);
            

            return CustomResponse(OrderDTO);
        }

    }
}
