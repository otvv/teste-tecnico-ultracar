using Ultracar.Dto;
using Ultracar.Context;

namespace Ultracar.Repository
{
  public class OrcamentoRepository : IOrcamentoRepository
  {
    private readonly IUltracarDbContext _context;
    public OrcamentoRepository(IUltracarDbContext context)
    {
      _context = context;
    }

    //

    public IEnumerable<OrcamentoDto> GetOrcamentos()
    {
      List<OrcamentoDto>? orcamentos = _context.Orcamentos.Select(orc => new OrcamentoDto
      {
        NumeracaoOrcamento = orc.NumeracaoOrcamento,
        PlacaVeiculo = orc.PlacaVeiculo,
        NomeCliente = orc.NomeCliente,
        Pecas = orc.Pecas,
      }).ToList();

      return orcamentos;
    }
  }

}