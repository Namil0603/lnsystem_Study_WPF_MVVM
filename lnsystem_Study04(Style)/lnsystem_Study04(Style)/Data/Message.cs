namespace lnsystem_Study04_Style_.Data
{
    public class Message
    {
        public Message(string? id, string? newMessage, bool isLocal)
        {
            Id = id;
            Chat = newMessage;
            IsLocal = isLocal;
        }

        public string? Id { get; set; }
        public string? Chat { get; set; }
        public bool IsLocal { get; set; }

        public override string ToString()
        {
            return $"{Id} : {Chat}";
        }
    }
}