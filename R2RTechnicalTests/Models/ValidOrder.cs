using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace R2RTechnicalTests.Models
{    
    [DataContract]
    public class ValidOrder
    {
        [DataMember]
        public int OrderNumber { get; set; }

        [DataMember(Name = "OrderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }

    }


    public class OrderDetail
    {
        public int ItemNumber { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
    }
}
