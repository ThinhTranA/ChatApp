
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.Web.Server
{
    public class ApplicationDbContext : DbContext
    {
        public string Id => Guid.NewGuid().ToString("N");

        #region Public Properties
        public DbSet<SettingsDataModel> Settings { get; set; }

        #endregion

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {

        }

   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API
            modelBuilder.Entity<SettingsDataModel>().HasIndex(a => a.Name);
        }
    }
}
