using System;
using Afi.Registration.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afi.Registration.UnitTests.Persistence
{
    public static class Utils
    {
        public static AfiDbContext SeedInMemDb(Action<AfiDbContext> seedAction = null)
        {
            var dbOptsBuilder = new DbContextOptionsBuilder<AfiDbContext>();
            
            // This connection string discards cache on disconnecting
            dbOptsBuilder.UseSqlite("DataSource=:memory:");

            var db = new AfiDbContext(dbOptsBuilder.Options);
            db.Database.OpenConnection();
            db.Database.EnsureCreated();

            seedAction?.Invoke(db);

            db.SaveChanges();
            return db;
        }
    }
}
