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

    public void AddOrUpdatePartInOrcamento(int orcamentoId, Peca peca)
    {
      Orcamento? orcamento = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .FirstOrDefault(orcament => orcament.Id == orcamentoId);

      if (orcamento == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to quote. (are you specifying the quote 'id' in the request body?)");
      }

      if (orcamento.Pecas == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, 'Peca' table is empty or null.");
      }

      // check if part to be added or modified already exists in the quote
      Peca? foundPart = orcamento.Pecas.FirstOrDefault(part => part.Id == peca.Id);

      // if it doesn't exist
      if (foundPart == null)
      {
        // add peca in the client's quote
        orcamento.Pecas.Add(peca);

        // save changes in the data base
        _context.SaveChanges();
      }
      else // if it does exist
      { 
        // update data of existing part 
        foundPart.EstoqueId = peca.EstoqueId;
        foundPart.OrcamentoId = peca.OrcamentoId;
        foundPart.Quantidade = peca.Quantidade;
        foundPart.NomePeca = peca.NomePeca;
      }

      // check if part to be added or modified exists in stock
      // if the part is valid, the API now has a valid instance of Estoque
      Estoque? foundPartInStock = _context.Estoque.FirstOrDefault(stock => stock.Id == peca.EstoqueId);

      // in case the stock is invalid
      if (foundPartInStock == null)
      {
        throw new InvalidOperationException($"[Ultracar] - ERROR: failed to validade stock, part with id: {peca.EstoqueId} was not found in stock.");
      }
      else
      {
        // NOTE: this below is a very subject rule, since in theory the establishment could make a quote, add the parts to it
        // but the client might not have authorized the order so the part could still be in stock

        // check if the part actually has stock available
        if (foundPartInStock.EstoquePeca > 0 || foundPartInStock.EstoquePeca >= peca.Quantidade)
        {
          // set part state to Reserve since it's being added into a clients quote
          foundPartInStock.TipoMovimentacao = ActionTypes.Reserved;

          // deduct the quantity of part in the stock
          foundPartInStock.EstoquePeca -= peca.Quantidade;
        }
        else
        {
          // set part status to out of stock
          foundPartInStock.TipoMovimentacao = ActionTypes.OutOfStock;

          throw new InvalidOperationException($"[Ultracar] - ERROR: failed to add part, the {foundPartInStock.NomePeca} stock is empty or it doesn't have the amount specified in the request.");
        }
      }
    }

    //

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
    public OrcamentoDto GetOrcamentoById(int id)
    { 
      Orcamento? orcamento = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .FirstOrDefault(orcament => orcament.Id == id);

      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
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
          EstoqueId = peca.EstoqueId,
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
        .Include(obj => obj.Pecas!)
        .ThenInclude(obj => obj.Estoque)
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
    public OrcamentoDto GetOrcamentoByNumber(string orcamentoNumber)
    { 
      Orcamento? orcamento = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .FirstOrDefault(orcament => orcament.NumeracaoOrcamento == orcamentoNumber);

      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
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
          EstoqueId = peca.EstoqueId,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList()
      };

      return result;
    }

    //

    public OrcamentoDto UpdateOrcamentoById(int id, Orcamento orcamentoBody)
    {   
     if (orcamentoBody == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // find quote to edit by its id
      Orcamento? updatedOrcamento = _context.Orcamentos
      .Include(obj => obj.Pecas!)
      .ThenInclude(obj => obj.Estoque)
      .FirstOrDefault(orcament => orcament.Id == id);

      if (updatedOrcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, quote not found.");
      }

      // partially edit quote with data received from body
      updatedOrcamento.NumeracaoOrcamento = orcamentoBody.NumeracaoOrcamento;
      updatedOrcamento.NomeCliente = orcamentoBody.NomeCliente;
      updatedOrcamento.PlacaVeiculo = orcamentoBody.PlacaVeiculo;

      if (orcamentoBody.Pecas != null)
      {
        // iterate through all the parts being added in the quote body
        foreach (Peca? peca in orcamentoBody.Pecas)
        {
          // update part in the quote
          AddOrUpdatePartInOrcamento(updatedOrcamento.Id, peca);
        }
      }

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

    //

    public OrcamentoDto CreateOrcamento(Orcamento newOrcamentoBody)
    {
      if (newOrcamentoBody == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to create, body is empty.");
      }

      if (newOrcamentoBody.Pecas == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to access the part(s) in quote, 'Pecas' table is empty or null.");
      }

      // populate dummy list to store current parts in the quote
      // before the api deletes them to handle the request
      List<Peca> pecasToAdd = newOrcamentoBody.Pecas;
      
      if (newOrcamentoBody.Pecas != null)
      {
        // remove parts from new quote body to avoid conflict
        newOrcamentoBody.Pecas = null;
      }

      // partialy populate current quote column with body content
      _context.Orcamentos.Add(newOrcamentoBody);

      // save changes in the data base
      _context.SaveChanges();

      if (pecasToAdd != null)
      {
        // iterate through all the parts being added in the quote body
        foreach (Peca? peca in pecasToAdd)
        {
          // add new part(s) in the quote
          AddOrUpdatePartInOrcamento(newOrcamentoBody.Id, peca);
        }
      }

      // save changes in the data base
      _context.SaveChanges();

      // create dto to display the created quote
      OrcamentoDto result = new()
      {
        Id = newOrcamentoBody.Id,
        NumeracaoOrcamento = newOrcamentoBody.NumeracaoOrcamento,
        NomeCliente = newOrcamentoBody.NomeCliente,
        PlacaVeiculo = newOrcamentoBody.PlacaVeiculo,
        Pecas = newOrcamentoBody.Pecas?.Select(peca => new PecaDto
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
      .FirstOrDefault(orcament => orcament.Id == id);

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
        Estoque? foundPartInStock = _context.Estoque.FirstOrDefault(stock => stock.Id == peca.EstoqueId);

        if (foundPartInStock != null)
        {
          // add the quantity of parts being removed back to the stock
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
