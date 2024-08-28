namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class Message
    {
        private string Sender { get; }
        private string Content { get; }
        private DateTime Timestamp { get; }

        public Message(string sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {Sender}: {Content}";
        }
    }
}