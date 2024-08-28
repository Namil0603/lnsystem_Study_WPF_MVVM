namespace lnsystem_Study02_UDP_Socket_.Model
{
    public class User
    {
        public string IpAddress { get; set; }
        public string Name { get; set; }

        public User(string ipAddress, string name)
        {
            IpAddress = ipAddress;
            Name = name;
        }
    }
}