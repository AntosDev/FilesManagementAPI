using Autofac;
using Autofac.Extensions.DependencyInjection;
using FilesManagement.Common.Application.InvertedDependencies;
using FilesManagement.Common.Infra.DataAccess;
using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain.InvertedDependencies;
using FilesManagement.Core.Infra.DataAccess;
using FilesManagement.Core.Infra.Services;
using FilesManagement.Infra.DataAccess.Context;
using Identity.Application.InvertedDependencies;
using Identity.Domain.InvertedDependencies;
using Identity.Domain.User;
using Identity.Infra;
using Identity.Infra.DataAccess;
using Identity.Infra.DataAccess.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);


var _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(typeof(Program));

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    #region Logger COnfig

    Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
    .CreateBootstrapLogger();

    builder.RegisterInstance(Log.Logger)
              .As<Serilog.ILogger>()
              .SingleInstance();

    #endregion

    builder.RegisterType<SqlConnectionFactory>()
          .As<ISqlConnectionFactory>()
          .WithParameter("connectionString", _configuration.GetConnectionString("MSSQL"))
          .InstancePerLifetimeScope();

    builder
          .Register(c =>
          {
              var dbContextOptionsBuilder = new DbContextOptionsBuilder<FMDbContext>();
              dbContextOptionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));



              return new FMDbContext(_configuration);
          })
          .AsSelf()
          .As<DbContext>()
          .InstancePerLifetimeScope();
    builder
      .Register(c =>
      {
          var dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityDBContext>();
          dbContextOptionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));



          return new IdentityDBContext(_configuration);
      })
      .AsSelf()
      .As<DbContext>()
      .InstancePerLifetimeScope();

    builder.RegisterType<UserRepository>().As<IUserRepository>();
    builder.RegisterType<FilesRepository>().As<IFilesRepository>();

    builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
    builder.RegisterType<TokenGenerator>().As<ITokenGenerator>();
    builder.RegisterType<FileManagerService>().As<IFileSystemHelper>();


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
