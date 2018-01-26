using Firebase.MessagePush.Models;
using FirebaseNet.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Firebase.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        
        [Route("api/values", Name = "post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Alert value)
        {
            var client = new FCMClient("AIzaSyChp172E9Q9oWg14_Ejy7H6Zr3x1SUCqwA");
            if (value.IsAndroid)
            {
                var info = new Dictionary<string, string>();
                info["Body"] = JsonConvert.SerializeObject(value); 
                    info["Title"] = "Alert"; 
                 var message = new Message()
                {
                    To = "GENERAL", 

                    Data = info
                };

                var result = await client.SendMessageAsync(message);
            }
            else
            {
                var message = new Message()
                {
                    To = "GENERAL",
                    Notification = new IOSNotification()
                    {
                        Body = JsonConvert.SerializeObject(value),
                        Title = "Alert"
                    }
                };

                var result = await client.SendMessageAsync(message);
            }
            return Ok();
        }
    }
}