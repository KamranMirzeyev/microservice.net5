﻿namespace ASP.NET_Web.Models
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientForUser { get; set; }
    }
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
