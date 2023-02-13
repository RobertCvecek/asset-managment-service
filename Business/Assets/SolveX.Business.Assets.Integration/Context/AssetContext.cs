

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SolveX.Business.Assets.Domain.Models;

namespace SolveX.Business.Assets.Integration.Context;

public class AssetContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AssetContext(IConfiguration configuration)
      : base()
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Default"));
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    //entities
    public DbSet<Asset> Asset { get; set; }
    public DbSet<AssetConnection> AssetConnection { get; set; }
}
