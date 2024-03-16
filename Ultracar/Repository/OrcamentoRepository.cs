using Ultracar.Dto;
using Ultracar.Context;
using Microsoft.EntityFrameworkCore;
using Ultracar.Models;

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

      if (orcamentos == null)
      {
        return null!;
      }

      return orcamentos;
    }
    public OrcamentoDto GetOrcamentoById(int id)
    { 
      Orcamento? orcamento = _context.Orcamentos
      .Include(obj => obj.Pecas)
      .FirstOrDefault(orcament => orcament.Id == id);

      if (orcamento == null) 
      {
        return null!;
      }

      OrcamentoDto result = new()
      {
        Id = orcamento!.Id,
        NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
        PlacaVeiculo = orcamento.PlacaVeiculo,
        NomeCliente = orcamento.NomeCliente,
        Pecas = orcamento.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList()
      };

      return result;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName)
    {
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas)
        .Where(orc => orc.NomeCliente == clientName)
        .Select(orcamento => new OrcamentoDto
        {
          Id = orcamento.Id,
          NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
          PlacaVeiculo = orcamento.PlacaVeiculo,
          NomeCliente = orcamento.NomeCliente,
          Pecas = orcamento.Pecas!.Select(peca => new PecaDto
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