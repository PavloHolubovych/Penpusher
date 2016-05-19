namespace Penpusher.Models
{
    public class UserNewsProviderModels
    {
        public int Id { get; set; }

        public int IdNewsProvider { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string RssImage { get; set; }

        public System.DateTime SubscriptionDate { get; set; }
    }
}