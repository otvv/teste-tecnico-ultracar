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
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
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

      if (orcamentos == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quote not found.");
      }

      return orcamentos;
    }
    public IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate)
    {
      List<OrcamentoDto> orcamentos = _context.Orcamentos
        .Include(obj => obj.Pecas)
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
            NomePeca = peca.NomePeca,
            Quantidade = peca.Quantidade,
            PecaEntregue = peca.PecaEntregue
          }).ToList()
        }).ToList();

      if (orcamentos == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quotes not found.");
      }

      return orcamentos;
    }
    public OrcamentoDto GetOrcamentoByNumber(string orcamentoNumber)
    { 
      Orcamento? orcamento = _context.Orcamentos
      .Include(obj => obj.Pecas)
      .FirstOrDefault(orcament => orcament.NumeracaoOrcamento == orcamentoNumber);

      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to find, quotes not found.");
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

    //

    public OrcamentoDto UpdateOrcamentoById(int id, Orcamento orcamento)
    {   
      if (orcamento == null)
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // find quote to edit by its id
      Orcamento? updatedOrcamento = _context.Orcamentos
      .Include(obj => obj.Pecas)
      .FirstOrDefault(orcament => orcament.Id == id);

      if (updatedOrcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, quotes not found.");
      }

      // edit quote
      _context.Orcamentos.Update(updatedOrcamento);

      // edit quote with data received from body
      updatedOrcamento.NumeracaoOrcamento = orcamento.NumeracaoOrcamento;
      updatedOrcamento.NomeCliente = orcamento.NomeCliente;
      updatedOrcamento.PlacaVeiculo = orcamento.PlacaVeiculo;

      // save changes in the data base
      _context.SaveChanges();
      
      // create a simple dto to display the changes
      // partialy at the moment
      OrcamentoDto result = new()
      {
        Id = updatedOrcamento.Id,
        NumeracaoOrcamento = updatedOrcamento.NumeracaoOrcamento,
        NomeCliente = updatedOrcamento.NomeCliente,
        PlacaVeiculo = updatedOrcamento.PlacaVeiculo,
        Pecas = updatedOrcamento.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }
    public OrcamentoDto UpdateOrcamento(Orcamento orcamento)
    { 
      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to update, body is empty.");
      }

      // edit an entire quote column
      _context.Orcamentos.Update(orcamento);

      // save changes in the data base
      _context.SaveChanges();

      // create a simple dto to display the changes
      // partialy at the moment
      OrcamentoDto result = new()
      {
        Id = orcamento.Id,
        NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
        NomeCliente = orcamento.NomeCliente,
        PlacaVeiculo = orcamento.PlacaVeiculo,
        Pecas = orcamento.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }

    public OrcamentoDto CreateOrcamento(Orcamento orcamento)
    {
      if (orcamento == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to create, body is empty.");
      }

      // populate quote table with body content
      _context.Orcamentos.Add(orcamento);

      // save changes in the data base
      _context.SaveChanges();

      // create a simple dto to display the created quote
      OrcamentoDto result = new()
      {
        Id = orcamento.Id,
        NumeracaoOrcamento = orcamento.NumeracaoOrcamento,
        NomeCliente = orcamento.NomeCliente,
        PlacaVeiculo = orcamento.PlacaVeiculo,
        Pecas = orcamento.Pecas?.Select(peca => new PecaDto
        {
          Id = peca.Id,
          NomePeca = peca.NomePeca,
          Quantidade = peca.Quantidade,
          PecaEntregue = peca.PecaEntregue
        }).ToList(),
      };

      return result;
    }

    public void RemoveOrcamento(int id)
    {
      // find quote to remove by its id
      Orcamento? orcamentoToRemove = _context.Orcamentos
      .Include(obj => obj.Pecas)
      .FirstOrDefault(orcament => orcament.Id == id);

      if (orcamentoToRemove == null) 
      {
        throw new InvalidOperationException("[Ultracar] - ERROR: failed to remove, orcamento not found.");
      }

      // remove orcament 
      _context.Orcamentos.Remove(orcamentoToRemove);

      // save changes in the data base
      _context.SaveChanges();
    }
  }
}
