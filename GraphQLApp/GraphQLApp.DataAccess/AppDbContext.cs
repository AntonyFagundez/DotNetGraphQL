using GraphQLApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options) {  }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

  
    }
}