namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class Message
    {
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public Message(string sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"<{Timestamp:HH:mm:ss}> {Sender}: {Content}";
        }
    }
}