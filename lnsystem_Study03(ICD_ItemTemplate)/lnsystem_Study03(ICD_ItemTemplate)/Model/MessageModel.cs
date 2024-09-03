using System.Collections.ObjectModel;

namespace lnsystem_Study03_ICD_ItemTemplate_.Model
{
    public class MessageModel
    {
        public static MessageModel Instance;
        public ObservableCollection<string> MessagesModel { get; }
        public string InputTextBox { get; set; }

        public MessageModel()
        {
            Instance = this;
            MessagesModel = new ObservableCollection<string>();
        }
    }
}
