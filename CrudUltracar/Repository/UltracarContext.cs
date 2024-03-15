using Microsoft.EntityFrameworkCore;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public class UltracarContext : DbContext, IUltracarContext
  {
    // models
    public DbSet<Orcamento> Orcamentos { get; set; } = null;
    public DbSet<Peca> Pecas { get; set; } = null;
    public DbSet<Estoque> Estoque { get; set; } = null; // relationship table between Peca and Estoque (1:N)

    // methods
    public UltracarContext(DbContextOptions<UltracarContext> options) : base(options) { }
    public UltracarContext() { }
  }
}
