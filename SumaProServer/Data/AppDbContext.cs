using Microsoft.EntityFrameworkCore;
using SumaProServer.Models;

namespace SumaProServer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Suma> Sumas => Set<Suma>();
}