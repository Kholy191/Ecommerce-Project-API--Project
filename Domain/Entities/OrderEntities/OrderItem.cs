using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered OrderedItem { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }

        #region Navigation Properties
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        #endregion
    }
}
