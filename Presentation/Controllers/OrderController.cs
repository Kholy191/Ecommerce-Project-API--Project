using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.OrderDtos;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderCreateDto _order)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderServices.CreateOrderAsync(_order, email);
            return Ok(order);
        }

        [HttpGet("GetDeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await serviceManager.OrderServices.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

        [HttpGet("GetOrdersForUser")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return NotFound();
            }
            var orders = await serviceManager.OrderServices.GetOrdersForUserAsync(email);
            return Ok(orders);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderServices.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}   
