using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Abstract;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Repository;

namespace OnlineLearning.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			//services.AddDbContext<ApplicationDatabaseContext>(opts => opts.UseInMemoryDatabase("database"));
			//services.AddScoped<ApplicationDatabaseContext>();

			services.AddDbContext<ApplicationDatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
