using System;

namespace AuthServer.MVC.Models
{
    public class AuthorPreviewDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}