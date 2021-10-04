using System;
using System.Linq;
using Afi.Registration.Api;
using Afi.Registration.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RefactorThis.IntegrationTests
{
    public class IntegrationTestingWebAppFactory : WebApplicationFactory<Startup>
    {
        private readonly string sqliteConnection;
        private readonly Action<AfiDbContext> seedAction;

        public IntegrationTestingWebAppFactory(
            string sqliteConnection = "DataSource=:memory:",
            Action<AfiDbContext> seedAction = null)
        {
            this.sqliteConnection = sqliteConnection;
            this.seedAction = seedAction;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AfiDbContext>));

                services.Remove(descriptor);
                services.AddDbContext<AfiDbContext>(options =>
                {
                    options.UseSqlite(sqliteConnection);
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AfiDbContext>();
                db.Database.OpenConnection();
                db.Database.EnsureCreated();
                
                db.Customers.RemoveRange(db.Customers);
                db.Policies.RemoveRange(db.Policies);
                db.SaveChanges();

                seedAction?.Invoke(db);
                db.SaveChanges();
            });
        }
    }
}
