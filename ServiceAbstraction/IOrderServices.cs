using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Shared.DeliveryMethodsDtos;
using Shared.OrderDtos;

namespace ServiceAbstraction
{
    public interface IOrderServices 
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderCreateDto order, string buyerEmail);
        public Task<List<DeliveryMethodsDto>> GetDeliveryMethodsAsync();
        public Task<List<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);
        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
