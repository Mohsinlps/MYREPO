

using BeajLearner.Application;
using BeajLearner.Application.Interfaces;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.Entities;
using BeajLearner.Domain.Interfaces;
using BeajLearner.Infrastructure.Identity;
using BeajLearner.Infrastructure.Identity.Models;
using BeajLearner.Infrastructure.Persistance;
using BeajLearner.Infrastructure.Persistance.Contexts;
using BeajLearner.Infrastructure.Persistance.Repositories;
using BeajLearner.Infrastructure.Shared;
using BeajLearner.WebApi.Extensions;

using BeajLearner.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;


IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter() },
    {"message_template", new MessageTemplateColumnWriter() },
    {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    {"exception", new ExceptionColumnWriter() },
    {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
    {"isAuditLog", new SinglePropertyColumnWriter("isAuditLog", PropertyWriteMethod.Raw,NpgsqlDbType.Boolean) }
};

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File("log.txt")
    .WriteTo.PostgreSQL(
    connectionString: _config.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
    tableName: _config.GetSection("Serilog:TableName").Value,columnWriters,needAutoCreateTable: true,respectCase: true,useCopy: false));


//Initialize Logger
Log.Logger = new LoggerConfiguration()
     .WriteTo.File("log.txt")
.WriteTo.PostgreSQL(
       connectionString: _config.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
       tableName: _config.GetSection("Serilog:TableName").Value,
       columnWriters,
       needAutoCreateTable: true,
       respectCase: true,
        useCopy: false)
    .CreateLogger();



// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("Trusted", p =>
    {
        p.AllowAnyHeader()

        .AllowAnyMethod()
        .WithOrigins(_config.GetValue<string>("AllowedOrigins").Split(','))
        .AllowCredentials();
    });
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
    {
        options.IdleTimeout=TimeSpan.FromMinutes(2);

    });
builder.Services.AddApplicationLayer();
builder.Services.AddIdentityInfrastructure(_config);
builder.Services.AddPersistenceInfrastructure(_config);
builder.Services.AddSharedInfrastructure(_config);
builder.Services.AddSwaggerExtension();
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IGetCategoriesRepository, GetCategoriesRepository>();
builder.Services.AddScoped<IGetLessonRepository, GetLessonRepository>();
builder.Services.AddScoped<IGetCourseRepository, GetCourseRepository>();
builder.Services.AddScoped<ITeachersManageRepository, TeachersManageRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ICustomerCourseManageRepository, CustomerCourseManageRepository>();

builder.Services.AddScoped<IAsyncRepository<CourseCategory>, EfRepository<CourseCategory>>();
builder.Services.AddScoped<IAsyncRepository<Course>, EfRepository<Course>>();
builder.Services.AddScoped<IAsyncRepository<Lesson>, EfRepository<Lesson>>();
builder.Services.AddScoped<IAsyncRepository<mcqQuestions>, EfRepository<mcqQuestions>>();
builder.Services.AddScoped<IAsyncRepository<Otp>, EfRepository<Otp>>();
builder.Services.AddScoped<IAsyncRepository<TeachersAssignedCourse>, EfRepository<TeachersAssignedCourse>>();
builder.Services.AddScoped<IAsyncRepository<purchasedCourse>, EfRepository<purchasedCourse>>();
builder.Services.AddScoped<IAsyncRepository<CourseWeek>, EfRepository<CourseWeek>>();
builder.Services.AddScoped<IAsyncRepository<ActivityAlias>, EfRepository<ActivityAlias>>();
builder.Services.AddScoped<IAsyncRepository<SpeakActivityQuestions>, EfRepository<SpeakActivityQuestions>>();
builder.Services.AddScoped<IAsyncRepository<DocumentFiles>, EfRepository<DocumentFiles>>();
builder.Services.AddScoped<IAsyncRepository<SpeakActivityQuestions>, EfRepository<SpeakActivityQuestions>>();

//builder.Services.AddScoped<IAsyncRepository<UserSignup>, EfRepository<UserSignup>>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Trusted", p =>
    {
        p.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(_config.GetValue<string>("AllowedOrigins").Split(','))
        .AllowCredentials();
    });
});





var app = builder.Build();

app.UseSession();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await BeajLearner.Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
        await BeajLearner.Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);

        //Log.Information("Finished Seeding Default Data");
        //Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occurred seeding the DB");
    }
    finally
    {
        //Log.CloseAndFlush();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "BeajLearner - Swagger UI";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeajLearner.WebApi");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
if (!Directory.Exists(_config["FileSetting:DirectoryPath"]))
    Directory.CreateDirectory(_config["FileSetting:DirectoryPath"]);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
    RequestPath = new PathString("/app-images")
});

app.UseCors("Trusted");

app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
