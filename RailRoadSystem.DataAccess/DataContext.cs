namespace RailRoadSystem.DataAccess
{
    using RailRoadSystem.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext,Migrations.Configuration>());
        }
        
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.FullName).IsUnique();
        }
    }
}