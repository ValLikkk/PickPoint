using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TestPickPoint.Models
{
    public class Order
    {
        public Order(OrderStatusEnum status)
        {
            OrderStatus = (int)status;
        }

        public int OrderNumber { get; set; }

        public int OrderStatus { get; }

        public string[] OrderList { get; set; }

        public decimal OrderCost { get; set; }

        public  int PostamatNumber { get; }

        public string CustomerPhoneNumber { get; set; }

        public string FullName { get; set; }
    }

    public enum OrderStatusEnum
    {
        Registered = 1,
        AcceptedInStock = 2,
        IssuedToCourier = 3,
        DeliveredCheckpoint = 4,
        DeliveredRecipient = 5,
        Canceled = 6
    }
}