using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TestPickPoint.Models;
using TestPickPoint.Services;

namespace TestPickPoint.Controllers
{
    public class PostamatController : ApiController
    {

        private PostamatRepository PostamatRepository ;

        public PostamatController()
        {
            this.PostamatRepository = new PostamatRepository();
        }
        [HttpGet]
        public HttpResponseMessage GetPostamat(string numberPostamat)
        {
            if (!this.PostamatRepository.CheckFormatPostamentNumber(numberPostamat))
                throw new HttpResponseException(HttpStatusCode.BadRequest);//некорректен, выдавать ошибку   запроса.
            if(!this.PostamatRepository.SearchPostamat(numberPostamat))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var result = this.PostamatRepository.GetPostamat(numberPostamat);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");
            return response; 
        }
    }
}
