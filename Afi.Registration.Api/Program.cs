using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Afi.Registration.Api
{
    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a new host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }
}
