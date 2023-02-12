using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SolveX.Business.Users.Domain.Models;

namespace SolveX.Business.Users.Integration.Context;

public class UserContext : DbContext
{
    private readonly IConfiguration _configuration;

    public UserContext(IConfiguration configuration)
      :base()
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
    public DbSet<UserModel> Users { get; set; }
}
