namespace ASP_NET_API.Model
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string hashPassword { get; set; }
        public bool isAdmin { get; set; }
    }
}