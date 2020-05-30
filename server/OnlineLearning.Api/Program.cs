using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineLearning.EntityFramework.Context;

namespace OnlineLearning.Api
{
	public class Program
	{
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetService<ApplicationDatabaseContext>();
          //var passwordHasher = services.GetService<IPasswordHasher>();
          //apply all migrations
          context.Database.Migrate();
          //seed data
          //DatabaseSeed.Seed(context, passwordHasher);
        }
        catch (Exception e)
        {
          throw e;
        }

      }
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
  }
}
