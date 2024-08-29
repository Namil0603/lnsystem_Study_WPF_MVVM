using Newtonsoft.Json;
using System;

namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class Message
    {
        public User Sender { get; }
        public string Content { get; }
        private DateTime Timestamp { get; }

        public Message(User sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
        }

        public override string ToString() => $"[{Timestamp:HH: mm: ss}] {Sender.Name} ({Sender.IpAddress}) : {Content}";

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Message FromJson(string json) => JsonConvert.DeserializeObject<Message>(json);
    }
}