
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Web.Server
{
    public class ApplicationDbContext : DbContext
    {
        #region Public Properties
        public DbSet<SettingsDataModel> Settings { get; set; }

        #endregion

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=entityframework;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
