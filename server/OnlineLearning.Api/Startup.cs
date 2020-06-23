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
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

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

      services.AddCors(options =>
     {
       options.AddPolicy("CorsPolicy",
         builder => builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
         .WithExposedHeaders("X-Pagination"));
     });

      //to avoid multipartbodylength error during upload
      services.Configure<FormOptions>(o =>
      {
        o.ValueLengthLimit = int.MaxValue;
        o.MultipartBodyLengthLimit = int.MaxValue;
        o.MemoryBufferThreshold = int.MaxValue;
      });


      services.AddControllers().ConfigureApiBehaviorOptions(options =>
      {
        // Adds a custom error response factory when ModelState is invalid
        options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
      });

      services.AddAuthorization(config =>
      {
        config.AddPolicy(Policies.SuperAdmin, Policies.SuperAdminPolicy());
        config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
        config.AddPolicy(Policies.User, Policies.UserPolicy());
      });

      services.AddDbContext<ApplicationDatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));
      //Repository Here
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ISchoolRepository, SchoolRepostiory>();
      services.AddScoped<ISessionRepository, SessionRepository>();
      services.AddScoped<ISessionReferenceRepository, SessionReferenceRepository>();
      services.AddScoped<ISessionStatusRepository, SessionStatusRepository>();
      services.AddScoped<ISectionRepository, SectionRepository>();
      services.AddScoped<IClassRepository, ClassRepository>();
      services.AddScoped<IParentRepository, ParentRepository>();
      services.AddScoped<IAttendanceRepository, AttendanceRepository>();
      services.AddScoped<IAssignmentRepository, AssignmentRepository>();
      services.AddScoped<IAssignmentSubmissionRepository, AssignmentSubmissionRepository>();
      //services.AddScoped<IGr, AssignmentSubmissionRepository>();
      services.AddScoped<IMessageMainRepository, MessageMainRepository>();
      services.AddScoped<IMessageReplyRepository, MessageReplyRepository>();
      services.AddScoped<IAssignmentSubmissionRepository, AssignmentSubmissionRepository>();
      services.AddScoped<IReferenceTypeRepository, ReferenceTypeRepository>();
      services.AddScoped<ISubmitAssignmentRepository, SubmitAssignmentRepository>();
      services.AddScoped<ISubmitAssignmentAttachmentsRepository, SubmitAssignmentAttachmentsRepository>();
      //services.AddScoped<Itecher, SubmitAssignmentRepository>();
      services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
      services.AddScoped<ISubmissionStatusRepository, SubmissionStatusRepository>();
      services.AddScoped<IStudentRepository, StudentRepository>();
      services.AddScoped<ITeacherRepository, TeacherRepository>();


      services.AddScoped<IUnitOfWork, UnitOfWork>();

      //Service Here

      services.AddScoped<IAuthenticationService, AuthenticationService>();
      services.AddScoped<IAssignmentService, AssignmentService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IStudentService, StudentService>();
      services.AddScoped<ITeacherService, TeacherService>();
      services.AddScoped<ISchoolService, SchoolService>();
      services.AddScoped<ISectionService, SectionService>();
      services.AddScoped<IClassService, ClassService>();
      services.AddScoped<IParentService, ParentService>();
      services.AddScoped<IAttendanceService, AttendanceService>();
      services.AddScoped<ISessionService, SessionService>();
      services.AddScoped<IAssignmentSubmissionService, AssignmentSubmissionService>();
      services.AddScoped<IAttendanceService, AttendanceService>();
      //services.AddScoped<IGrade, SubmitAssignmentRepository>();
      services.AddScoped<IMessageMainService, MessageMainService>();
      services.AddScoped<IMessageReplyService, MessageReplyService>();
      services.AddScoped<IReferenceTypeService, ReferenceTypeService>();
      services.AddScoped<ISessionService, SessionService>();
      services.AddScoped<ISessionReferenceService, SessionReferenceService>();
      services.AddScoped<ISessionStatusService, SessionStatusService>();
      services.AddScoped<ISubjectService, SubjectService>();
      services.AddScoped<ISubmissionStatusService, SubmissionStatusService>();
      services.AddScoped<ISubmitAssignmentService, SubmitAssignmentService>();
      services.AddScoped<ISubmitAssignmentAttachmentsService, SubmitAssignmentAttachmentsService>();
      services.AddScoped<ITeacherService, TeacherService>();
      services.AddScoped<IListService, ListService>();


      services.AddSingleton<IPasswordHasher, PasswordHasher>();
      services.AddSingleton<Shared.Interface.Security.Tokens.ITokenHandler, Shared.Security.Tokens.TokenHandler>();



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

      app.UseCors("CorsPolicy");

      app.UseStaticFiles(); //enables using static files for the request. If we don’t set a path to the static files, it will use a wwwroot folder in our solution explorer by default.
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
        RequestPath = new PathString("/Resources")
      });


      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}