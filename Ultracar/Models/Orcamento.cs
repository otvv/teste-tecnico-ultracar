using System.ComponentModel.DataAnnotations;

namespace Ultracar.Models
{
  public class Orcamento // one to many relationship with Pecas table
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
