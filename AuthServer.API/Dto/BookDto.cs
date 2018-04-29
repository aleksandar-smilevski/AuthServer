using System;
using System.Collections.Generic;

namespace AuthServer.API.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DatePublished { get; set; }    
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public List<AuthorPreviewDto> Authors { get; set; }
        public List<string> Orders { get; set; }
    }
}