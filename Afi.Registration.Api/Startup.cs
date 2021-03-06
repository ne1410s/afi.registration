using System;
using Afi.Registration.Api.Middleware;
using Afi.Registration.Api.Models;
using Afi.Registration.Api.Services;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Repositories;
using Afi.Registration.Domain.Services;
using Afi.Registration.Persistence;
using Afi.Registration.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Afi.Registration.Api
{
    /// <summary>
    /// The web application startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The web host environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the web host environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Called by the runtime to add services to the container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Afi Registration Api",
                Version = "v1"
            }));

            var connectionString = Configuration.GetConnectionString(
                "AfiDbConnection");
            services.AddDbContext<AfiDbContext>(options =>
            {
                if (Environment.IsDevelopment())
                {
                    options.UseSqlite(connectionString);
                }
                else
                {
                    options.UseSqlServer(connectionString);
                }
            });

            services.AddTransient<
                IItemValidator<CustomerRegistrationRequest>,
                RegistrationRequestValidator>();
            services.AddTransient<
                IItemValidator<Customer>,
                CustomerValidator>();
            services.AddTransient<
                ICustomerRequestMapper,
                CustomerRequestMapper>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPolicyRepository, PolicyRepository>();
            services.AddTransient<ICustomerRegistrar, CustomerRegistrar>();
        }

        /// <summary>
        /// Called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Afi Registration Api v1");

                if (env.IsProduction())
                {
                    // Disable all calls via UI
                    c.SupportedSubmitMethods(Array.Empty<SubmitMethod>());
                }
            });


            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandler>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
