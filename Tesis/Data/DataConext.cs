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
        public DbSet<TrustUser> Trusts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //here begins the configuration of the self referencing trust and trusted user relationship
            modelBuilder.Entity<TrustUser>()
                .HasKey(k => new { k.SourceUserId, k.TrustedUserId });

            modelBuilder.Entity<TrustUser>()
                .HasOne(s => s.SourceUser)
                .WithMany(t => t.TrustedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrustUser>()
                .HasOne(s => s.TrustedUser)
                .WithMany(t => t.TrustedbyUsers)
                .HasForeignKey(s => s.TrustedUserId)
                .OnDelete(DeleteBehavior.NoAction);
            //here it ends the configuration of the self referencing trust and trusted user relationship    


            modelBuilder.Entity<REProperty>()
                        .HasMany<Photo>(property => property.Photos)
                        .WithOne(photo => photo.Property)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
