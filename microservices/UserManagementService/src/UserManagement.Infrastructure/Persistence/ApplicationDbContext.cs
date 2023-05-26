using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #region Tables

        public DbSet<ApplicationUser> ApplicationUsers { get; set;}



        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            #region Seed Data
            // TODO:
            #endregion
            
            base.OnModelCreating(builder);
        }
    }
}
