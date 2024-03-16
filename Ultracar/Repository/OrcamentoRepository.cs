using Ultracar.Dto;
using Ultracar.Context;
using Microsoft.EntityFrameworkCore;

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
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas)
        .Select(orcament => new OrcamentoDto
        {
            Id = orcament.Id,
            NumeracaoOrcamento = orcament.NumeracaoOrcamento,
            PlacaVeiculo = orcament.PlacaVeiculo,
            NomeCliente = orcament.NomeCliente,
            Pecas = orcament.Pecas!.Select(peca => new PecaDto
            {
                Id = peca.Id,
                NomePeca = peca.NomePeca,
                Quantidade = peca.Quantidade,
                PecaEntregue = peca.PecaEntregue
            }).ToList()
        }).ToList();

      return orcamentos;
    }
  }

}