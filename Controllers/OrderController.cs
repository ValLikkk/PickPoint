using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web.Helpers;
using System.Web.Http;
using TestPickPoint.Models;
using TestPickPoint.Services;

namespace TestPickPoint.Controllers
{
    public class OrderController : ApiController
    {

        private OrderRepository orderRepository;
        private PostamatRepository postamatRepository;

        public OrderController()
        {
            this.orderRepository = new OrderRepository();
            this.postamatRepository = new PostamatRepository();
        }

        [HttpPost]
        public HttpResponseMessage CreateOrder(Order order)
        {
            var chechk = CkeckError(order);
            if (chechk == HttpStatusCode.OK)
            {
                if(this.orderRepository.SaveOrder(order))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            throw new HttpResponseException(chechk);
        }
  

        [HttpPatch]
        public HttpResponseMessage ChangeOrder(Order order)
        {
            var chechk = CkeckError(order);
            if (chechk == HttpStatusCode.OK)
            {
                if (this.orderRepository.ChangeOrder(order))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            throw new HttpResponseException(chechk);
        }

        [HttpGet]
        public HttpResponseMessage GetOrderInformation(int orderNumber)         
        {
            if (!this.orderRepository.ExistsOrder(orderNumber))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);//«не найден».
            }
            var result = this.orderRepository.GetOrder(orderNumber);    
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");
            return response;
        }

        [HttpDelete]
        public HttpResponseMessage CalcelOrder(int orderNumber)
        {
            if (this.orderRepository.CancelOrder(orderNumber))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private HttpStatusCode CkeckError(Order order)
        {
            if (!this.postamatRepository.SearchPostamat(order.PostamatNumber.ToString()))
            {
                return HttpStatusCode.BadRequest;  //выдавать ошибку запроса.
            }
            if (this.orderRepository.CheckPhoneNumber(order.CustomerPhoneNumber))
            {
                return HttpStatusCode.BadRequest; //«ошибка запроса».
            }
            if (this.postamatRepository.CheckStatusPostamat(order.PostamatNumber))
            {
                return HttpStatusCode.Forbidden; //«запрещено».
            }
            if(!this.orderRepository.ExistsOrder(order.OrderNumber))
            {
                return HttpStatusCode.NotFound;//«не найден».
            }
            if(!this.orderRepository.CountLiimitOrderList(order))
            {
                return HttpStatusCode.BadRequest;
            }
            if (!this.postamatRepository.CheckFormatPostamentNumber(order.PostamatNumber.ToString()))
            {
                return HttpStatusCode.BadRequest;
            }
            if (!this.postamatRepository.SearchPostamat(order.PostamatNumber.ToString()))
                return HttpStatusCode.NotFound;//«не найден».
            return HttpStatusCode.OK;
        }
    }
}
