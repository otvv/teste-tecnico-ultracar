using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultracar.Models;

public enum ActionTypes : int
{
  InStock,
  Reserved,
}

// estoque model (many-to-one relationship db table)
public class Estoque
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string? NomePeca { get; set; }
  [Required]
  public int Quantidade { get; set; }

  // type of the stock operation
  // could be either "InStock" or "Reserved"
  [Required]
  public ActionTypes TipoMovimentacao { get; set; }
}

