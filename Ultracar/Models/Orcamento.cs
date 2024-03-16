using System.ComponentModel.DataAnnotations;

namespace Ultracar.Models
{
  // orcamento model (one-to-many db table)
  public class Orcamento 
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string? NumeracaoOrcamento { get; set; }
    [Required]
    public string? PlacaVeiculo { get; set; }
    [Required]
    public string? NomeCliente { get; set; }
    public List<Peca>? Pecas { get; set; }
  }
} 
