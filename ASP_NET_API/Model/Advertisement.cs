namespace ASP_NET_API.Model
{
    public class Advertisement
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int price { get; set; }
        public string date { get; set; }
        public bool vip { get; set; }
        public int regionId { get; set; }
        public int headingId { get; set; }
    }
}
