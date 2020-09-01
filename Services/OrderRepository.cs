using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Results;
using TestPickPoint.Models;

namespace TestPickPoint.Services
{
    public class OrderRepository
    {
        private static List<Order> ListOrders = new List<Order>();
        
        private PostamatRepository postamatRepository;

        public OrderRepository()
        {
            this.postamatRepository = new PostamatRepository();
        }
        public bool SaveOrder(Order order)
        {        
            try
            {
                ListOrders.Add(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        public bool ExistsOrder(int orderNumber)
        {
            if (ListOrders.Exists(x => x.OrderNumber == orderNumber))
            {
                return true;
            }
            return false;//«не найден».
        }

        public bool CountLiimitOrderList(Order order)
        {
            if(order.OrderList.Length > 10)
            {
                return true;
            }
            return false;
        }
        public bool ChangeOrder(Order order)
        {
            try
            {             
                var index = ListOrders.FindIndex(x => x.OrderNumber == order.OrderNumber);
                ListOrders[index] = order;
                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool CancelOrder(int orderNumber)
        {
            try
            {
              
                var index = ListOrders.FindIndex(x => x.OrderNumber == orderNumber);
                var currentOrder = ListOrders[index];
                Order cancelOrder = new Order(OrderStatusEnum.Canceled) 
                { 
                    CustomerPhoneNumber = currentOrder.CustomerPhoneNumber, 
                    FullName = currentOrder.FullName, 
                    OrderCost = currentOrder.OrderCost, 
                    OrderList = currentOrder.OrderList,
                    OrderNumber = currentOrder.OrderNumber };
                ListOrders[index] = cancelOrder;
                return true;
            }
            catch
            {
                return false;
            }
         
        }

        public Order GetOrder(int orderNumber)
        {
            return ListOrders.FirstOrDefault(x => x.OrderNumber == orderNumber);
        }
        public bool CheckPhoneNumber(string customerPhoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(customerPhoneNumber))
                    return false;
                var r = new Regex(@"^(7(\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
                return r.IsMatch(customerPhoneNumber);
            }
            catch
            {
                return false;
            }

        }
    }
}