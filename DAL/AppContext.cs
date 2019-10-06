using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using FileDelivery.Models;

namespace FileDelivery.DAL
{
    public class AppContext : DbContext
    {

        public DbSet<Entry> Entries;
        public DbSet<Upload> Uploads;


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
