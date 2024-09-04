namespace lnsystem_Study03_ICD_ItemTemplate_.Data
{
    public class Message
    {
        public Message(string id, string newMessage, bool isSentByMe)
        {
            ID = id;
            Chat = newMessage;
            IsSentByMe = isSentByMe;
        }

        public string ID { get; set; }
        public string Chat { get; set; }
        public bool IsSentByMe { get; set; }

        public override string ToString()
        {
            return $"{ID} : {Chat}";
        }
    }
}