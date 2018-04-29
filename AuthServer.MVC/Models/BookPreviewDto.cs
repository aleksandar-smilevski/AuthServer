using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.MVC.Models
{
    public class BookPreviewDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public DateTime? DatePublished { get; set; }
        public decimal? Price { get; set; }
        public string ISBN { get; set; }
    }
}