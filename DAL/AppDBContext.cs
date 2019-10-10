using FileDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace FileDelivery.DAL
{
    /**
     * <see cref="DbContext"/> implementation for application database.
     * This contains required application tables.
     * 
     * <see cref="AppDBContext"/> should not be used directly, but an implementation of <see cref="IDataService"/> should be used.
     * This will abstract the underlying database structure from application code.
     * For instance, the backing database might be changed completely from Entity framework and we only have to change the <see cref="IDataService"/> implementation.
     */
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasIndex(e => new { e.EmailAddress, e.TransactionID})
                .IsUnique(true);
        }

        public DbSet<Entry> Entries;
        public DbSet<Upload> Uploads;
    }
}
