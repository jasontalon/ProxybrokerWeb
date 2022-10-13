using Microsoft.EntityFrameworkCore;
using Proxybroker.Domain.Entities;

namespace Proxybroker.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Proxy> Proxies { get; set; }
}