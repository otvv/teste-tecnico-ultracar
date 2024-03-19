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

    //

    public void HandleStock(Peca peca)
    {
      if (peca == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to validade stock, part argument is empty or null.");
      }

       // check if part to be added or modified exists in stock
      // if the part is valid, the API now has a valid instance of Estoque
      Estoque? foundPartInStock = _context.Estoque.SingleOrDefault(stock => stock.Id == peca.EstoqueId);

      // in case the stock is invalid
      if (foundPartInStock == null)
      {
        throw new InvalidOperationException($"[Ultracar] - ERROR: failed to validade stock, part with id: {peca.EstoqueId} was not found in stock.");
      }
      else
      {
        // NOTE: this below is a very subject rule, since in theory the establishment could make a quote, add the parts to it
        // but the client might not have authorized the order so the part could still be in stock but somehow reserved
        // the way I'm doing is just cutting the "middle-man" so the part goes to the client's quote right after its created,
        // even in the case that doesn't approve it. In this case, the API can safely delete his quote and the parts will go back 
        // to the stock. 

        // check if the part actually have a valid stock quantity available
        if (foundPartInStock.EstoquePeca > 0 || foundPartInStock.EstoquePeca >= peca.Quantidade)
        {
          // set part state to Reserved since it's being added into a clients quote
          foundPartInStock.TipoMovimentacao = ActionTypes.Reserved;

          // deduct the quantity of part in the stock
          foundPartInStock.EstoquePeca -= peca.Quantidade;

          // update part name (just in case)
          peca.NomePeca = foundPartInStock.NomePeca; 
        }
        else
        {
          // set part status to OutOfStock (in case it isn't already)
          foundPartInStock.TipoMovimentacao = ActionTypes.OutOfStock;

          throw new InvalidOperationException($"[Ultracar] - ERROR: failed to add part, the {foundPartInStock.NomePeca} stock is empty or it doesn't have the amount specified in the request.");
        }
      }
    }

    public IEnumerable<OrcamentoDto> GetOrcamentos()
    {
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
        .Select(orcament => new OrcamentoDto
        {
            Id = orcament.Id,
            NumeracaoOrcamento = orcament.NumeracaoOrcamento,
            PlacaVeiculo = orcament.PlacaVeiculo,
            NomeCliente = orcament.NomeCliente,
            Pecas = orcament.Pecas!.Select(peca => new PecaDto
            {
                Id = peca.Id,
                EstoqueId = peca.EstoqueId,
                NomePeca = peca.Estoque!.NomePeca,
                Quantidade = peca.Quantidade,
                PecaEntregue = peca.PecaEntregue
            }).ToList()
        }).ToList();

      if (orcamentos == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote(s) not found.");
      }

      return orcamentos;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoById(int id)
    { 
      List<OrcamentoDto> orcamento = _context.Orcamentos
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
        .Where(orcament => orcament.Id == id)
        .Select(orcamento => new OrcamentoDto
        {
          Id = orcamento.Id,
          NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
          PlacaVeiculo = orcamento.PlacaVeiculo,
          NomeCliente = orcamento.NomeCliente,
          Pecas = orcamento.Pecas!.Select(peca => new PecaDto
          {
            Id = peca.Id,
            EstoqueId = peca.EstoqueId,
            NomePeca = peca.Estoque!.NomePeca,
            Quantidade = peca.Quantidade,
            PecaEntregue = peca.PecaEntregue
          }).ToList()
        }).ToList();

      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
      }

      return orcamento;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName)
    {
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
        .Where(orcament => orcament.NomeCliente == clientName)
        .Select(orcamento => new OrcamentoDto
        {
          Id = orcamento.Id,
          NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
          PlacaVeiculo = orcamento.PlacaVeiculo,
          NomeCliente = orcamento.NomeCliente,
          Pecas = orcamento.Pecas!.Select(peca => new PecaDto
          {
            Id = peca.Id,
            EstoqueId = peca.EstoqueId,
            NomePeca = peca.Estoque!.NomePeca,
            Quantidade = peca.Quantidade,
            PecaEntregue = peca.PecaEntregue
          }).ToList()
        }).ToList();

      if (orcamentos == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote(s) not found.");
      }

      return orcamentos;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate)
    {
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
        .Where(orc => orc.PlacaVeiculo == licensePlate)
        .Select(orcamento => new OrcamentoDto
        {
          Id = orcamento.Id,
          NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
          PlacaVeiculo = orcamento.PlacaVeiculo,
          NomeCliente = orcamento.NomeCliente,
          Pecas = orcamento.Pecas!.Select(peca => new PecaDto
          {
            Id = peca.Id,
            EstoqueId = peca.EstoqueId,
            NomePeca = peca.Estoque!.NomePeca,
            Quantidade = peca.Quantidade,
            PecaEntregue = peca.PecaEntregue
          }).ToList()
        }).ToList();

      if (orcamentos == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote(s) not found.");
      }

      return orcamentos;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoByNumber(string orcamentoNumber)
    {
      List<OrcamentoDto> orcamento = _context.Orcamentos
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
        .Where(orcament => orcament.NumeracaoOrcamento == orcamentoNumber)
        .Select(orcamento => new OrcamentoDto
        {
          Id = orcamento.Id,
          NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
          PlacaVeiculo = orcamento.PlacaVeiculo,
          NomeCliente = orcamento.NomeCliente,
          Pecas = orcamento.Pecas!.Select(peca => new PecaDto
          {
            Id = peca.Id,
            EstoqueId = peca.EstoqueId,
            NomePeca = peca.Estoque!.NomePeca,
            Quantidade = peca.Quantidade,
            PecaEntregue = peca.PecaEntregue
          }).ToList()
        }).ToList();

      if (orcamento == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
      }

      return orcamento;
    }

    //

    public OrcamentoDto UpdateOrcamentoInfo(int id, Orcamento orcamentoInfoBody)
    {   
     if (orcamentoInfoBody == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // find quote to edit by its id
      Orcamento? updatedOrcamento = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .SingleOrDefault(orcament => orcament.Id == id);

      if (updatedOrcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, quote not found.");
      }

      // partially edit quote with data received from body
      updatedOrcamento.NumeracaoOrcamento = orcamentoInfoBody.NumeracaoOrcamento;
      updatedOrcamento.NomeCliente = orcamentoInfoBody.NomeCliente;
      updatedOrcamento.PlacaVeiculo = orcamentoInfoBody.PlacaVeiculo;

      // save changes in the data base
      _context.SaveChanges();
      
      // create dto to display the changes
      OrcamentoDto result = new()
      {
        Id = updatedOrcamento.Id,
        NumeracaoOrcamento = updatedOrcamento.NumeracaoOrcamento,
        NomeCliente = updatedOrcamento.NomeCliente,
        PlacaVeiculo = updatedOrcamento.PlacaVeiculo,
        Pecas = updatedOrcamento.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          EstoqueId = peca.EstoqueId,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }

    public OrcamentoDto UpdatePartsInOrcamento(int id, List<Peca> pecasToEditFromBody)
    {
      if (pecasToEditFromBody == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to edit, request body is empty.");
      }

      // find quote to edit by its id
      Orcamento? orcamentoToEdit = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .SingleOrDefault(orcament => orcament.Id == id);

      if (orcamentoToEdit == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to edit, quote not found.");
      }

      if (orcamentoToEdit.Pecas == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in quote, 'Pecas' table is empty or null.");
      }

      foreach (Peca? peca in pecasToEditFromBody)
      {
        if (peca == null)
        {
          throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in request body. parts array is empty or null.");
        }

        // find part to edit
        Peca? foundPecaToEdit = orcamentoToEdit.Pecas.SingleOrDefault(part => part.EstoqueId == peca.EstoqueId);

        if (foundPecaToEdit == null)
        {
          throw new InvalidOperationException("[Ultracar] - ERROR: failed to edit. part to edit was not found.");
        }

        // update parts in the current quote with info from request body
        // NOTE: user won't be able to update sensitive info such as the part Id or its EstoqueId (stock Id)
        foundPecaToEdit.NomePeca = peca.NomePeca;
        foundPecaToEdit.Quantidade = peca.Quantidade;

        // TODO: maybe handle name changes? 
        // check in the Estoque table the new name belongs to a part in stock, if so add it to stock and remove the old one
        // (this will need new methods, such as one to remove a single part from the quote...)

        // handle stock changes
        HandleStock(peca);
      }

      // save changes in the data base
      _context.SaveChanges();

      // generate dto with result
      OrcamentoDto result = new()
      {
        Id = orcamentoToEdit.Id,
        NumeracaoOrcamento = orcamentoToEdit.NumeracaoOrcamento,
        NomeCliente = orcamentoToEdit.NomeCliente,
        PlacaVeiculo = orcamentoToEdit.PlacaVeiculo,
        Pecas = orcamentoToEdit.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          EstoqueId = peca.EstoqueId,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }
    //

    public InsertOrcamentoDto CreateOrcamentoInfo(Orcamento newOrcamentoInfoBody)
    {
      if (newOrcamentoInfoBody == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to create, body is empty.");
      }

      // populate current quote column with body content
      _context.Orcamentos.Add(newOrcamentoInfoBody);

      // save changes in the data base
      _context.SaveChanges();

      // create dto to display the created quote
      InsertOrcamentoDto result = new()
      {
        Id = newOrcamentoInfoBody.Id,
        NumeracaoOrcamento = newOrcamentoInfoBody.NumeracaoOrcamento,
        NomeCliente = newOrcamentoInfoBody.NomeCliente,
        PlacaVeiculo = newOrcamentoInfoBody.PlacaVeiculo,
      };

      return result;
    }
    public OrcamentoDto AddPartsInOrcamento(int id, List<Peca> pecasFromBody)
    {
     if (pecasFromBody == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to add, request body is empty.");
      }

      // find quote to add parts by its id
      Orcamento? orcamentoToEdit = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .SingleOrDefault(orcament => orcament.Id == id);

      if (orcamentoToEdit == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to edit, quote not found.");
      }

      if (orcamentoToEdit.Pecas == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in quote, 'Pecas' table is empty or null.");
      }

      foreach (Peca? peca in pecasFromBody)
      {
        if (peca == null)
        {
          throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in request body. parts array is empty or null.");
        }

        // check if part already exists somewhere else (if it's in another quote)
        bool pecaExists = _context.Pecas.Any(part => part.Id == peca.Id);

        // if it already exists, throw an error
        if (pecaExists)
        {
          throw new InvalidOperationException($"[Ultracar] - ERROR: failed to add part. part with id: {peca.Id} already exists in another quote, please provide an unique id.");
        }

        // create a new instance
        Peca pecaToAdd = new()
        {
          Id = peca.Id,
          EstoqueId = peca.EstoqueId,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
        };

        // add part in client's quote
        orcamentoToEdit.Pecas.Add(peca);

        // handle stock changes
        HandleStock(peca);
      }

      // save changes in the data base
      _context.SaveChanges();

      // generate dto with result
      OrcamentoDto result = new()
      {
        Id = orcamentoToEdit.Id,
        NumeracaoOrcamento = orcamentoToEdit.NumeracaoOrcamento,
        NomeCliente = orcamentoToEdit.NomeCliente,
        PlacaVeiculo = orcamentoToEdit.PlacaVeiculo,
        Pecas = orcamentoToEdit.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          EstoqueId = peca.EstoqueId,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }
    //

    public void RemoveOrcamento(int id)
    {
      // find quote to remove by its id
      Orcamento? orcamentoToRemove = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .SingleOrDefault(orcament => orcament.Id == id);

      if (orcamentoToRemove == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to remove, quote not found.");
      }

      if (orcamentoToRemove.Pecas == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in quote, 'Pecas' table is empty or null.");
      }

      foreach (Peca? peca in orcamentoToRemove.Pecas)
      {
        // find part by its id in stock
        Estoque? foundPartInStock = _context.Estoque.SingleOrDefault(stock => stock.Id == peca.EstoqueId);

        // in case the stock is invalid
        if (foundPartInStock == null)
        {
          throw new InvalidOperationException($"[Ultracar] - ERROR: failed to validade stock, part with id: {peca.EstoqueId} was not found in stock.");
        }
        else
        {
          // add the number of stock from the parts being removed back to the stock
          foundPartInStock.EstoquePeca += peca.Quantidade;

          // set part state to InStock since it's being returned or 
          // if the part was out of stock because it got placed in a client's quote
          foundPartInStock.TipoMovimentacao = ActionTypes.InStock;
        }
      }

      // remove quote
      _context.Orcamentos.Remove(orcamentoToRemove);

      // save changes in the data base
      _context.SaveChanges();
    }
  }
}
