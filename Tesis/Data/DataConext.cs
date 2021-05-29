using Microsoft.EntityFrameworkCore;
using Tesis.Entities;

namespace Tesis.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base (options){}
        public DbSet<AppUser> Users { get; set; }
        public DbSet<REProperty> Properties { get; set; }
         
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    //modelBuilder.Entity<REProperty>()
        //    //    .HasKey(p => p.Id);
        //    //modelBuilder.Entity<AppUser>()
        //    //    .HasKey(u => u.Id);
        //    //modelBuilder.Entity<REProperty>()
        //    //    .HasOne(p => p.Agent)
        //    //    .WithMany(u => u.Properties)
        //    //    .HasForeignKey(u => u.Id);

        //    modelBuilder.Entity<REProperty>()
        //        .HasOne(p => p.Agent)
        //        .WithMany(u => u.Properties)
        //        .HasForeignKey(p => p.Id);
        //}
    }
}
