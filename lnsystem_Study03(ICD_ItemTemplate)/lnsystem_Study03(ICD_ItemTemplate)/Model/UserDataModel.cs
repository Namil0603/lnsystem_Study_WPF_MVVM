namespace lnsystem_Study03_ICD_ItemTemplate_.Model
{
    public class UserDataModel
    {
        public static UserDataModel Instance;
        public UserDataModel() => Instance = this;
        
        public string LocalID;
    }
}
