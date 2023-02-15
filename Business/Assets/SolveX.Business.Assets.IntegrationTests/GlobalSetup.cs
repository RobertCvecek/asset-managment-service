using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SolveX.Business.Assets.ApplicationServices;
using SolveX.Business.Users.Integration;
using SolveX.Framework.Integration;
using SolveX.Framework.TestingUtility.DependencyInjectionTests;
using System.Reflection;
using System.Data.SQLite;
using SolveX.Business.Assets.Integration.Context;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace SolveX.Business.Assets.IntegrationTests;

[SetUpFixture]
public class GlobalSetup
{
    private readonly List<Autofac.Module> modules = new List<Autofac.Module>
    {
        new FrameworkIntegrationModule(),
        new AssetIntegrationModule(),
        new AssetApplicationModule(),
        new AssetDomainModule()
    };

    IConfigurationRoot configuration;

    [OneTimeSetUp]
    public void Setup()
    {
        configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(
            path: "appsettings.json",
            optional: false,
            reloadOnChange: true)
        .Build();

        List<Assembly> listOfAssemblies = new();
        var mainAsm = Assembly.GetEntryAssembly();
        listOfAssemblies.Add(mainAsm);

        foreach (var refAsmName in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)))
            .Where(assembly => assembly.GetName().Name.StartsWith("SolveX.Business.Assets")))
        {
            listOfAssemblies.Add(refAsmName);
        }

        Autofac.IContainer container = InjectionBasedTest.Init(models: modules, configuration: configuration, autoMapperConfigs: listOfAssemblies);

        SetupDatabase(container);
    }

    private string connectionString => configuration.GetConnectionString("Default");

    public void SetupDatabase(Autofac.IContainer container)
    {
        ///Create database
        SQLiteConnection.CreateFile(connectionString.Replace("Data Source=", ""));
        var assetContext = container.Resolve<AssetContext>();
        assetContext.Database.Migrate();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        FileInfo fi = new FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), connectionString.Replace("Data Source=", "")));
        if (fi.Exists ) 
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            fi.Delete();
        }
    }
}
