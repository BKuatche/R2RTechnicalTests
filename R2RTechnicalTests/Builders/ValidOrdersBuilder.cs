using AutoFixture;
using R2RTechnicalTests.Models;
using System;
using System.Collections.Generic;
using static R2RTechnicalTests.Models.ValidOrder;

namespace R2RTechnicalTests.Builders
{
    public class ValidOrdersBuilder
    {
        private readonly Fixture _fixture;

        public ValidOrdersBuilder()
        {
            _fixture = new Fixture();
        }


        public ValidOrder CreateValidOrdersInstance 
            => _fixture
            .Build<ValidOrder>()
            .With(m =>m.OrderNumber , 1)
            .With(m =>m.OrderDetails ,new List<OrderDetail> 
            {
             new OrderDetail{ ItemNumber = 1, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =1 , Cost = 1.00},
             new OrderDetail{ ItemNumber = 2, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =2 , Cost = 5.00},
             new OrderDetail{ ItemNumber = 3, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =5 , Cost = 10.00},
             new OrderDetail{ ItemNumber = 8, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =10 , Cost = 1.00},
             new OrderDetail{ ItemNumber = 9, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =2 , Cost = 50.00},
             new OrderDetail{ ItemNumber = 10, CustomerNumber =1, OrderDate = Convert.ToDateTime("02-02-2022") , Quantity =4 , Cost = 1000.00},
             new OrderDetail{ ItemNumber = 1, CustomerNumber =2, OrderDate = Convert.ToDateTime("04-02-2022") , Quantity =1 , Cost = 1.00},
             new OrderDetail{ ItemNumber = 2, CustomerNumber =2, OrderDate = Convert.ToDateTime("05-02-2022") , Quantity =2 , Cost = 5.00},
             new OrderDetail{ ItemNumber = 3, CustomerNumber =2, OrderDate = Convert.ToDateTime("06-02-2022") , Quantity =5 , Cost = 10.00}
            
            })
            .Create();
    }
}
