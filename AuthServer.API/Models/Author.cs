using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.API.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<BookAuthor> Books { get; set; }
    }
}