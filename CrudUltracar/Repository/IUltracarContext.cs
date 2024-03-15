using Microsoft.EntityFrameworkCore;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IUltracarContext
  {
    public DbSet<Orcamento> Orcamentos { get; set; }
    public DbSet<Peca> Pecas { get; set; }
    public DbSet<Estoque> Estoque { get; set; }
    public int SaveChanges();
  }
}
