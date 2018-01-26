using Firebase.MessagePush.Models;
using FirebaseNet.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Firebase.Api.Web.Controllers
{
    public class ValuesController : ApiController
    {
        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAJk2sCkg:APA91bGEr-eF3mxVqfr_-bo9JFiStCDVTVaKCIbvJGEDmEEgqXQL59-JbcpCStyG2mkUlt4KchuT0_85U0Vo1-yOTgwFv6sObO_YCzmyZI7ct8wh19YCExsO1SokTc11lE4BLgqsh_Jv");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "164511877704");

                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }
        // POST api/values


        [Route("api/values", Name = "post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Alert value)
        {
            var client = new FCMClient("AAAAJk2sCkg:APA91bGEr-eF3mxVqfr_-bo9JFiStCDVTVaKCIbvJGEDmEEgqXQL59-JbcpCStyG2mkUlt4KchuT0_85U0Vo1-yOTgwFv6sObO_YCzmyZI7ct8wh19YCExsO1SokTc11lE4BLgqsh_Jv");
            //await NotifyAsync("/topics/general", value.Title, JsonConvert.SerializeObject(value));
            if (value.IsAndroid)
            {
                var info = new Dictionary<string, string>();
                info["Body"] = JsonConvert.SerializeObject(value);
                info["Title"] = value.Title;
                var message = new Message()
                {
                   /* Notification = new AndroidNotification()
                    {
                        Body = JsonConvert.SerializeObject(value),
                        Title = value.Title
                    },*/
                    To = "/topics/general",
                    Data = info
                };

                var result = await client.SendMessageAsync(message);
            }
            else
            {
                var message = new Message()
                {
                    To = "/topics/general",// "/topics/all",
                    Notification = new IOSNotification()
                    {
                        Body = JsonConvert.SerializeObject(value),
                        Title = "Alert"
                    }
                };

                var result = await client.SendMessageAsync(message);
            }//*/
            return Ok();
        }
        
    }
}
