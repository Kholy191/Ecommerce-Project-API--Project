using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Entities.ProductEntities;
using ServiceAbstraction;
using Services.Exceptions_Implementation;
using Services.Specification_Implementation;
using Shared.DeliveryMethodsDtos;
using Shared.OrderDtos;

namespace Services
{
    public class OrderServices(IUnitOfWork unitOfWork, IMapper mapper,
        IBasketRepository basketRepository) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderCreateDto order, string buyerEmail)
        {
            var orderRepo = unitOfWork.GetRepository<Order , Guid>();
            var basket = await basketRepository.GetBasketAsync(order.BasketId) ?? throw new NoBasketFoundException(order.BasketId);

            #region Order Items Setting and SubTotal
            List<OrderItem> items = new List<OrderItem>();
            var productRepo = unitOfWork.GetRepository<Product, int>();
            decimal Subtotal = 0;
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id) ?? throw new NoProductFoundException(item.Id);
                var orderedProduct = mapper.Map<ProductItemOrdered>(product);
                items.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    OrderedItem = orderedProduct
                });
                Subtotal += product.Price * item.Quantity;
            }
            #endregion

            #region Delivery Method 
            var DeliveryMethod =  await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(order.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException();
            #endregion

            #region Creating Order
            var Order = new Order()
            {
                OrderItems = items,
                DeliveryMethod = DeliveryMethod,
                ShipToAddress = mapper.Map<ShippingAddress>(order.Address),
                UserEmail = buyerEmail,
                SubTotal = Subtotal,
            };

            await orderRepo.AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            #endregion

            return mapper.Map<OrderToReturnDto>(Order);
        }

        public async Task<List<DeliveryMethodsDto>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync().ContinueWith(t => mapper.Map<List<DeliveryMethodsDto>>(t));
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = orderRepo.GetByIdAsync(id);
            if (order == null) throw new OrderNotFoundException(id);
            return order.ContinueWith(o => mapper.Map<OrderToReturnDto>(o));
        }

        public async Task<List<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var spec = new OrderByEmailSpecification(buyerEmail);
            return await orderRepo.GetAllAsync(spec).ContinueWith(O => mapper.Map<List<OrderToReturnDto>>(O));
        }


    }
}
