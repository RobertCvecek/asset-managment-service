using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using SolveX.Business.Users.ApplicationServices;
using SolveX.Business.Users.Domain;
using SolveX.Business.Users.Integration;
using SolveX.Business.Users.Integration.Context;
using SolveX.Framework.Integration;
using SolveX.Framework.WebAPI.Middleware;
using SolveX.Framework.WebAPI.Models;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SolveX.AssetManagment;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public ILifetimeScope AutofacContainer { get; private set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod();
                builder.WithOrigins("http://localhost:8081")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddSwaggerGen();

        services.AddOptions();

        SecurityOptions securityOptions = new();
        Configuration.Bind(nameof(SecurityOptions), securityOptions);
        services.AddSingleton(securityOptions);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(t =>
        {
            t.SaveToken = true;
            t.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityOptions.IssuerSigningKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });

        services.AddAuthorization();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolveX.AssetManagment", Version = "v1" });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme }
                    },
                    new List<string>()
                }
            });
        });
    }

    // ConfigureContainer is where you can register things directly
    // with Autofac. This runs after ConfigureServices so the things
    // here will override registrations made in ConfigureServices.
    // Don't build the container; that gets done for you by the factory.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        // Register your own things directly with Autofac here. Don't
        // call builder.Populate(), that happens in AutofacServiceProviderFactory
        // for you.
        //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

        ILoggerFactory logFactroy = LoggerFactory.Create(config =>
        {
            config.ClearProviders();
            config.SetMinimumLevel(LogLevel.Trace);
            config.AddNLogWeb();
        });

        builder.RegisterInstance(logFactroy)
           .As<ILoggerFactory>()
           .SingleInstance();

        builder.RegisterGeneric(typeof(Logger<>))
               .As(typeof(ILogger<>))
               .SingleInstance();

        builder.RegisterModule(new FrameworkIntegrationModule());
        builder.RegisterModule(new UserApplicationModule());
        builder.RegisterModule(new UserDomainModule());
        builder.RegisterModule(new UserIntegrationModule());

        List<Assembly> listOfAssemblies = new List<Assembly>();
        var mainAsm = Assembly.GetEntryAssembly();
        listOfAssemblies.Add(mainAsm);
        foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
        {
            listOfAssemblies.Add(Assembly.Load(refAsmName));
        }

        var config = new MapperConfiguration(cfg => {
            cfg.AddMaps(listOfAssemblies);
        });

        builder.RegisterAutoMapper(false, listOfAssemblies.ToArray());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService <UserContext> ();
            context.Database.Migrate();
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asset managment API v1"));
        }
        AutofacContainer = app.ApplicationServices.GetAutofacRoot();

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints
            .MapControllers()
            .RequireAuthorization();
        });
    }
}
