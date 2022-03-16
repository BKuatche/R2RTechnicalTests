using System.Collections.Generic;

namespace R2RTechnicalTests.Models
{
    public class Orders
    {
        public class OrderDetail
        {
            public string ItemNumber { get; set; }
            public string CustomerNumber { get; set; }
            public string OrderDate { get; set; }
            public string Quantity { get; set; }
            public string Cost { get; set; }
        }


        public class Order
        {
            public string OrderNumber { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
        }

    }
}
