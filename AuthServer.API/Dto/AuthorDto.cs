using System.Collections.Generic;

namespace AuthServer.API.Dto
{
    public class AuthorDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookPreviewDto> Books { get; set; }
    }
}