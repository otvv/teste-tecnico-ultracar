using System.ComponentModel.DataAnnotations;

namespace Ultracar.Models;

public enum ActionTypes : int
{
  InStock,
  Reserved,
  OutOfStock,
}

public class Estoque // unidirectional relationship with Pecas table.
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string? NomePeca { get; set; }
  [Required]
  public int EstoquePeca { get; set; }

  // type of the stock operation
  // could be either "InStock", "Reserved" or "OutOfStock
  // TODO: add an invalid state for fallback reasons?
  [Required]
  public ActionTypes TipoMovimentacao { get; set; }
}

