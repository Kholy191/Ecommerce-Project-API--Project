using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;

namespace Services.Specification_Implementation
{
    public class OrderByEmailSpecification : Specification<Order, Guid>
    {
        public OrderByEmailSpecification(string email) : base(o => o.UserEmail == email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
        }
    }
}
