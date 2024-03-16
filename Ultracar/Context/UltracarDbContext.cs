using Microsoft.EntityFrameworkCore;
using Ultracar.Models;

namespace Ultracar.Context 
{
  public class UltracarDbContext : DbContext, IUltracarDbContext
  {
    public DbSet<Orcamento> Orcamentos { get; set; }
    public DbSet<Peca> Pecas { get; set; }
    public DbSet<Estoque> Estoque { get; set; }
    public UltracarDbContext(DbContextOptions<UltracarDbContext> options) : base(options) { }
    public UltracarDbContext() { }
  }
}
