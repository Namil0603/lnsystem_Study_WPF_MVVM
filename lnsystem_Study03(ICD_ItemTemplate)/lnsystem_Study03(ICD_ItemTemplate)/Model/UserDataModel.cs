namespace lnsystem_Study03_ICD_ItemTemplate_.Model
{
    public class UserDataModel
    {
        public static UserDataModel Instance;
        public string LocalID;

        public UserDataModel()
        {
            Instance = this;
        }
    }
}
