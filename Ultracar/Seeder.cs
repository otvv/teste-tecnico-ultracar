using Ultracar.Context;
using Ultracar.Models;

namespace Ultracar
{
  public class Seeder
  {
    public static void Initialize(UltracarDbContext context)
    {
      // check if the database has been seeded
      // if so, dont do anything
      if (context.Orcamentos.Any())
      {
        return;
      }

      Estoque[] estoque =
      [
        new() { NomePeca = "Peca1", EstoquePeca = 100, TipoMovimentacao = ActionTypes.InStock },
        new() { NomePeca = "Peca2", EstoquePeca = 200, TipoMovimentacao = ActionTypes.Reserved },
        new() { NomePeca = "Peca3", EstoquePeca = 2, TipoMovimentacao = ActionTypes.Reserved },
        new() { NomePeca = "Peca4", EstoquePeca = 0, TipoMovimentacao = ActionTypes.Reserved },
      ];
      
      // intermediate table
      Peca[] pecas =
      [
        new() { Quantidade = 1, NomePeca = "Peca1", OrcamentoId = 1, EstoqueId = 1 },
        new() { Quantidade = 3, NomePeca = "Peca2", OrcamentoId = 2, EstoqueId = 2 },
        new() { Quantidade = 2, NomePeca = "Peca3", OrcamentoId = 1, EstoqueId = 3 },
        new() { Quantidade = 1, NomePeca = "Peca4", OrcamentoId = 3, EstoqueId = 4 },
      ];

      Orcamento[] orcamentos =
      [
        new() { NumeracaoOrcamento = "112", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
        new() { NumeracaoOrcamento = "223", PlacaVeiculo = "XYZ5678", NomeCliente = "Jane Smith" },
        new() { NumeracaoOrcamento = "334", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
      ];

      context.Estoque.AddRange(estoque);
      context.Pecas.AddRange(pecas);
      context.Orcamentos.AddRange(orcamentos);

      // save changes in DB
      context.SaveChanges();
    }
  }
}
