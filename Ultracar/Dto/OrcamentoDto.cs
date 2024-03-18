namespace Ultracar.Dto 
{
  public class OrcamentoDto 
  {
    public int Id { get; set; }
    public string? NumeracaoOrcamento { get; set; }
    public string? PlacaVeiculo { get; set; }
    public string? NomeCliente { get; set; }
    public List<PecaDto>? Pecas { get; set; }
  }

  public class InsertOrcamentoDto
  {
    public int Id { get; set; }
    public string? NumeracaoOrcamento { get; set; }
    public string? PlacaVeiculo { get; set; }
    public string? NomeCliente { get; set; }
  }
}
