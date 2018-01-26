using System;

namespace Firebase.MessagePush.Models
{
    public class Alert
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public int Distance { get; set; }
    }
}