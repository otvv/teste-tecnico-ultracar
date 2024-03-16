using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultracar.Models;

// peca model (one-to-one db table)
public class Peca
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string? NomePeca { get; set; }
  [Required]
  public int Quantidade { get; set; }
  public int? OrcamentoId { get; set; }
  [ForeignKey("OrcamentoId")]
  public Orcamento? Orcamento { get; set; }
  public int? EstoqueId { get; set; }
  [ForeignKey("EstoqueId")]
  public Estoque? Estoque { get; set; }
  public bool PecaEntregue { get; set; }
}
