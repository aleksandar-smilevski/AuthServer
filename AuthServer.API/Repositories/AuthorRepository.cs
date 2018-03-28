using System;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;

namespace AuthServer.API.Repositories
{
    public class AuthorRepository : BaseRepository<Author, Guid>
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}