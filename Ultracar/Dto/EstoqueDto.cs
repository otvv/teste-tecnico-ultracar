using Ultracar.Models;

namespace Ultracar.Dto 
{
  public class EstoqueDto 
  {
    public int Id { get; set; }
    public string? NomePeca { get; set; }
    public int EstoquePeca { get; set; }
    public ActionTypes? TipoMovimentacao { get; set; }
  }
}
