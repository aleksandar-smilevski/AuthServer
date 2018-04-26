using System;

namespace AuthServer.API.Dto
{
    public class AuthorPreviewDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}