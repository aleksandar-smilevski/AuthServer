using System;

namespace AuthServer.MVC.Models
{
    public class BookPreviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }
    }
}