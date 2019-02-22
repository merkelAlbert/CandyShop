namespace ConsoleCandyShop.DAL
{
    public class User
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public User(string name = "", string phone = "")
        {
            Name = name;
            Phone = phone;
        }
    }
}