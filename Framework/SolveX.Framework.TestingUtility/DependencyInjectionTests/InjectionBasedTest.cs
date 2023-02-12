using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SolveX.Framework.Integration;
using System.Reflection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AutoMapper;

namespace SolveX.Framework.TestingUtility.DependencyInjectionTests;

/// <summary>
/// Testing base used for tests that require dependency injection
/// </summary>
public class InjectionBasedTest
{
    public static IContainer _container { get; set; }

    private static IConfiguration _configuration { get; set; }

    /// <summary>
    /// Method for initialization of dependency injection testing class
    /// </summary>
    /// <param name="models">Modules that contain all necessary components and mappings for successful injection</param>
    /// <param name="configuration">Optional configuration parameter</param>
    public static void Init(ICollection<Autofac.Module> models,
                            IConfiguration configuration = null,
                            IDictionary<Type, object> singletons = null,
                            List<Assembly> autoMapperConfigs = null)

    {
        ContainerBuilder builder = new();
        IServiceCollection services = new ServiceCollection();

        builder.RegisterInstance(configuration).As<IConfiguration>();
        _configuration = configuration;

        ILoggerFactory logFactroy = LoggerFactory.Create(config =>
        {
            config.ClearProviders();
            config.AddConsole();
            config.SetMinimumLevel(LogLevel.Trace);
        });

        builder.RegisterInstance(logFactroy)
           .As<ILoggerFactory>()
           .SingleInstance();

        builder.RegisterGeneric(typeof(Logger<>))
               .As(typeof(ILogger<>))
               .SingleInstance();


        builder.RegisterModule(new FrameworkIntegrationModule());

        if(autoMapperConfigs != null)
        {

            var config = new MapperConfiguration(cfg => {
                cfg.AddMaps(autoMapperConfigs);
            });

            builder.RegisterAutoMapper(false, autoMapperConfigs.ToArray());
        }

        foreach (Autofac.Module module in models)
        {
            builder.RegisterModule(module);
        }

        if (singletons != null)
        {
            foreach (var kvp in singletons)
            {
                builder.RegisterInstance(kvp.Value).As(kvp.Key);
            }
        }

        _container = builder.Build();
    }

    /// <summary>
    /// Retrieves a service from context
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>Component instance that provides the service</returns>
    public T Resolve<T>() where T : class
    {
        return _container.Resolve<T>();
    }

    public string GetConnectionString(string name = "Default")
    {
        return _configuration.GetConnectionString(name) ?? String.Empty;
    }
}
