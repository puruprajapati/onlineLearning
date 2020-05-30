using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineLearning.Api.Configuration
{
	public interface IConfigure
	{
		void ConfigureServices(IServiceCollection services, IConfiguration configuration);
	}
}
