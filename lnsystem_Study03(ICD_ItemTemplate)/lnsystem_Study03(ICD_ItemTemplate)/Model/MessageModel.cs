using System.Collections.ObjectModel;
using lnsystem_Study03_ICD_ItemTemplate_.Data;

namespace lnsystem_Study03_ICD_ItemTemplate_.Model
{
    public class MessageModel
    {
        public static MessageModel Instance;
        public ObservableCollection<Message> MessagesModel { get; }
        public string InputTextBox { get; set; }

        public MessageModel()
        {
            Instance = this;
            MessagesModel = new ObservableCollection<Message>();
        }
    }
}