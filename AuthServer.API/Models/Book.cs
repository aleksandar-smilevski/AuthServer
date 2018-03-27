using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.API.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }
        public virtual ICollection<BookAuthor> Authors { get; set; }
    }
}