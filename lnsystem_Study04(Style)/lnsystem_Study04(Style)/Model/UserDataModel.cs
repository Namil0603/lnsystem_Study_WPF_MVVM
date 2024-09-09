namespace lnsystem_Study04_Style_.Model
{
    public class UserDataModel
    {
        public static UserDataModel Instance;
        public UserDataModel() => Instance = this;
        
        public string LocalID;
    }
}
