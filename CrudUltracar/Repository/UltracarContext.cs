using Microsoft.EntityFrameworkCore;

namespace Ultracar.Context
{
  public class MyDbContext : DbContext
  {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
  }
}
