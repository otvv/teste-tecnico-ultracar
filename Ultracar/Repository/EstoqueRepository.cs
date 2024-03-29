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
      .SingleOrDefault(stock => stock.Id == id);

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

      // find part to edit by its part id inside the stock
      Estoque? updatedPart = _context.Estoque
      .SingleOrDefault(part => part.Id == id);

      if (updatedPart == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, part not found.");
      }

      // edit part in stock
      _context.Estoque.Update(updatedPart);

      // edit part with data received from body
      updatedPart.Id = partBody.Id;
      updatedPart.NomePeca = partBody.NomePeca;
      updatedPart.EstoquePeca = partBody.EstoquePeca;
      updatedPart.TipoMovimentacao = partBody.TipoMovimentacao;

      // save changes in the data base
      _context.SaveChanges();
      
      // create a simple dto to display the changes
      EstoqueDto result = new()
      {
        Id = updatedPart.Id,
        NomePeca = updatedPart.NomePeca,
        EstoquePeca = updatedPart.EstoquePeca,
        TipoMovimentacao = updatedPart.TipoMovimentacao,
      };

      return result;
    }
    public List<EstoqueDto> UpdateEstoque(List<Estoque> estoqueBody)
    { 
      if (estoqueBody == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // create dummy list to be populated later
      List<EstoqueDto> result = [];

      // iterate over body to add parts one at a time in a dummy dto list
      foreach (Estoque? estoque in estoqueBody)
      {
        if (estoque == null)
        {
          throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, table is empty.");
        }

        // edit an entire stock column
        _context.Estoque.Update(estoque);

        EstoqueDto partInStock = new()
        {
          Id = estoque.Id,
          NomePeca = estoque.NomePeca,
          EstoquePeca = estoque.EstoquePeca,
          TipoMovimentacao = estoque.TipoMovimentacao,
        };

        // populate result list
        result.Add(partInStock);
      }

      // save changes in the data base
      _context.SaveChanges();

      // returns entire modified stock as result
      return result;
    }

    //

    public EstoqueDto AddPartInEstoque(Estoque newPartBody)
    {
      if (newPartBody == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to create, body is empty.");
      }

      // check if a part with the same name already exists in the stock table
      bool partExists = _context.Estoque.Any(stock => stock.NomePeca == newPartBody.NomePeca);

      if (partExists)
      {
        throw new InvalidOperationException($"[Ultracar] - ERROR: part '{newPartBody.NomePeca}' already exists in the stock.");
      }

      // in case the user adds a part with multiple quantities
      if (newPartBody.EstoquePeca > 0)
      {
        // whenever a new part is added it will be added to stock by default
        newPartBody.TipoMovimentacao = ActionTypes.InStock;

        // populate quote table with body content
        _context.Estoque.Add(newPartBody);

        // save changes in the data base
        _context.SaveChanges();

        // create a simple dto to display the created quote
        EstoqueDto result = new()
        {
          Id = newPartBody.Id,
          NomePeca = newPartBody.NomePeca,
          EstoquePeca = newPartBody.EstoquePeca,
          TipoMovimentacao = newPartBody.TipoMovimentacao,
        };
        
        return result;
      }
      else // if the api user doesnt provide a valid quantity
      {
        // the api will assume that the user is trying to add one part to the stock 
        newPartBody.EstoquePeca = 1;

        // whenever a new part is added it will be added to stock by default
        newPartBody.TipoMovimentacao = ActionTypes.InStock;

        // populate quote table with body content
        _context.Estoque.Add(newPartBody);

        // save changes in the data base
        _context.SaveChanges();

        // dto result
        return new()
        {
          Id = newPartBody.Id,
          NomePeca = newPartBody.NomePeca,
          EstoquePeca = newPartBody.EstoquePeca,
          TipoMovimentacao = newPartBody.TipoMovimentacao,
        };
      }
    }
    public EstoqueDto AddStockToPartById(int id, int quantity)
    {
      // find part to edit by its part id inside the stock
      Estoque? updatedPart = _context.Estoque
      .SingleOrDefault(part => part.Id == id);

      if (updatedPart == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, part not found.");
      }

      // save original stock quantity for later checks
      int originalValue = updatedPart.EstoquePeca;

      // check if quantity to add is valid
      if (quantity > 0)
      {
        // increase part stock quantity with data received from the request query
        updatedPart.EstoquePeca += quantity;

        // if part is now in-stock, remove OutOfStock state
        if (originalValue == 0 && updatedPart.EstoquePeca > 0)
        { 
          updatedPart.TipoMovimentacao = ActionTypes.InStock;
        }

        // save changes in the data base
        _context.SaveChanges();
      }

      // create a simple dto to display the changes
      EstoqueDto result = new()
      {
        Id = updatedPart.Id,
        NomePeca = updatedPart.NomePeca,
        EstoquePeca = updatedPart.EstoquePeca,
        TipoMovimentacao = updatedPart.TipoMovimentacao,
      };

      return result;
    }
    public EstoqueDto RemoveStockFromPartById(int id, int quantity)
    {
      // find part to edit by its part id inside the stock
      Estoque? updatedPart = _context.Estoque
      .SingleOrDefault(part => part.Id == id);

      if (updatedPart == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, part not found.");
      }

      // decrease quantity from the part stock with data received from request query
      if (updatedPart.EstoquePeca > 0 || updatedPart.EstoquePeca >= quantity)
      {
        updatedPart.EstoquePeca -= quantity;
      }
      else 
      { 
        // if the quantity reaches 0 the part is out of stock
        updatedPart.TipoMovimentacao = ActionTypes.OutOfStock;

        throw new InvalidOperationException($"[Ultracar] - ERROR: failed to add part, the {updatedPart.NomePeca} stock is empty or it doesn't have the amount specified in the request.");
      }

      // save changes in the data base
      _context.SaveChanges();

      // create a simple dto to display the changes
      EstoqueDto result = new()
      {
        Id = updatedPart.Id,
        NomePeca = updatedPart.NomePeca,
        EstoquePeca = updatedPart.EstoquePeca,
        TipoMovimentacao = updatedPart.TipoMovimentacao,
      };

      return result;
    }

    //

    public void RemovePartFromEstoque(int id)
    {
      // find part to remove by its id
      Estoque? partToRemove = _context.Estoque
      .SingleOrDefault(part => part.Id == id);

      if (partToRemove == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to remove, part not found.");
      }

      // remove orcament 
      _context.Estoque.Remove(partToRemove);

      // save changes in the data base
      _context.SaveChanges();
    }
  }
}
