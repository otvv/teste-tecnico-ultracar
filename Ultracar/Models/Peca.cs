using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultracar.Models;

public class Peca // one-to-one relationship with Estoque table
{
  [Key]
  public int Id { get; set; }
  [Required]
  public int Quantidade { get; set; }
  [Required]
  public string? NomePeca { get; set; }
  public int? OrcamentoId { get; set; }
  [ForeignKey("OrcamentoId")]
  public Orcamento? Orcamento { get; set; }
  public int? EstoqueId { get; set; }
  [ForeignKey("EstoqueId")]
  public Estoque? Estoque { get; set; }
  
  // not mapped in the db
  [NotMapped] 
  public bool PecaEntregue
  {
    get
    {
      // if stock is null or part status is: InStock (0), return false
      if (Estoque == null || Estoque.TipoMovimentacao == ActionTypes.InStock)
      {
        return false;
      }
      
      // return false in case the part is out of stock
      if (Estoque.TipoMovimentacao == ActionTypes.OutOfStock)
      {
        return false;
      }

      // otherwise its reserved and thus true
      return true;
    }
  }
}
