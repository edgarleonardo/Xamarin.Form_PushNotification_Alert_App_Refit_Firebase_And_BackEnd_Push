using System;

namespace Firebase.MessagePush.Models
{
    public class Alert
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Distance { get; set; }
        public bool IsAndroid { get; set; }
        public Alert()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
