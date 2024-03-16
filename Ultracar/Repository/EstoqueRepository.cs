using Ultracar.Dto;
using Ultracar.Context;
using Microsoft.EntityFrameworkCore;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public class EstoqueRepository : IEstoqueRepository
  {
    private readonly IUltracarDbContext _context;
    public EstoqueRepository(IUltracarDbContext context)
    {
      _context = context;
    }

    //

    public IEnumerable<EstoqueDto> GetEstoque()
    {
      List<EstoqueDto> estoque = _context.Estoque
        .Select(stock => new EstoqueDto
        {
            Id = stock.Id,
            NomePeca = stock.NomePeca,
            EstoquePeca = stock.EstoquePeca,
            TipoMovimentacao = stock.TipoMovimentacao,
        }).ToList();

      if (estoque == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, stock not found.");
      }

      return estoque;
    }
    public EstoqueDto GetPartById(int id)
    { 
      Estoque? part = _context.Estoque
      .FirstOrDefault(stock => stock.Id == id);

      if (part == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, part not found.");
      }

      EstoqueDto result = new()
      {
        Id = part.Id,
        NomePeca = part.NomePeca,
        EstoquePeca = part.EstoquePeca,
        TipoMovimentacao = part.TipoMovimentacao,
      };

      return result;
    }
  }
}