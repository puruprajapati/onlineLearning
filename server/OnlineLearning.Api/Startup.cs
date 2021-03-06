using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineLearning.Api.Configuration;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Repository;
using OnlineLearning.Shared.Interface.Security;
using OnlineLearning.Shared.Security;
using AutoMapper;
using OnlineLearning.Service.Interface;
using OnlineLearning.Service;

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

			services.ConfigureServicesInAssembly(Configuration);

			services.AddAuthorization(config =>
			{
				config.AddPolicy(Policies.SuperAdmin, Policies.SuperAdminPolicy());
				config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
				config.AddPolicy(Policies.User, Policies.UserPolicy());
			});

			services.AddDbContext<ApplicationDatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IUserService, UserService>();

			services.AddSingleton<IPasswordHasher, PasswordHasher>();
			services.AddSingleton<Shared.Interface.Security.Tokens.ITokenHandler, Shared.Security.Tokens.TokenHandler>();
			
			services.AddControllers();

			services.AddAutoMapper(typeof(Startup));

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

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
