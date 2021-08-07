using Microsoft.EntityFrameworkCore;
using Tesis.Entities;

namespace Tesis.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base (options){}
        public DbSet<AppUser> Users { get; set; }
        public DbSet<REProperty> Properties { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<REProperty>()
                        .HasMany<Photo>(property => property.Photos)
                        .WithOne(photo => photo.Property)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
