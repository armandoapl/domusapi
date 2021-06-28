using Microsoft.EntityFrameworkCore;
using Tesis.Entities;

namespace Tesis.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base (options){}
        public DbSet<AppUser> Users { get; set; }
        public DbSet<REProperty> Properties { get; set; }

        
    }
}
