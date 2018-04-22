﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.API.Dto
{
    public class AuthorDto
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<BookPreviewDto> Books { get; set; }
    }
}