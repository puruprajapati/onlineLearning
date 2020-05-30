
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Configuration
{
	public static class ConfigurationExtension
	{
    public static void ConfigureServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
      var configs = typeof(Startup).Assembly.ExportedTypes.Where(x =>
          typeof(IConfigure).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IConfigure>().ToList();

      configs.ForEach(config => config.ConfigureServices(services, configuration));
    }
  }
}
