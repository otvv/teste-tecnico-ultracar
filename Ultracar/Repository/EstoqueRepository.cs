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
     public EstoqueDto GetPartByName(string partName)
    { 
      Estoque? part = _context.Estoque
      .FirstOrDefault(stock => stock.NomePeca == partName);

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
    public IEnumerable<EstoqueDto> GetPartsByState(ActionTypes state)
    {
      List<EstoqueDto> parts = _context.Estoque
      .Where(stock => stock.TipoMovimentacao == state)
      .Select(part => new EstoqueDto
      {
        Id = part.Id,
        NomePeca = part.NomePeca,
        EstoquePeca = part.EstoquePeca,
        TipoMovimentacao = part.TipoMovimentacao,
      }).ToList();

      if (parts == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, part(s) not found.");
      }

      return parts;
    }

    //

    public EstoqueDto UpdatePartById(int id, Estoque partBody)
    {   
      if (partBody == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // find stock to edit by its id
      Estoque? updatedParte = _context.Estoque
      .FirstOrDefault(part => part.Id == id);

      if (updatedParte == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, part not found.");
      }

      // edit part in stock
      _context.Estoque.Update(updatedParte);

      // edit quote with data received from body
      updatedParte.NomePeca = partBody.NomePeca;
      updatedParte.EstoquePeca = partBody.EstoquePeca;
      updatedParte.TipoMovimentacao = partBody.TipoMovimentacao;

      // save changes in the data base
      _context.SaveChanges();
      
      // create a simple dto to display the changes
      // partialy at the moment
      EstoqueDto result = new()
      {
        Id = updatedParte.Id,
        NomePeca = updatedParte.NomePeca,
        EstoquePeca = updatedParte.EstoquePeca,
        TipoMovimentacao = updatedParte.TipoMovimentacao,
      };

      return result;
    }
  }
}
