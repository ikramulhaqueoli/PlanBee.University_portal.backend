using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PlanBee.University_portal.backend.Repositories;

public partial class UniversityDb : DbContext
{
    private readonly IConfiguration _configuration;

    public UniversityDb(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
    }
    
    public UniversityDb(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
}