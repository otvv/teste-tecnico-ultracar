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

      Orcamento[] orcamentos =
      [
        new() { NumeracaoOrcamento = "112", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
        new() { NumeracaoOrcamento = "223", PlacaVeiculo = "XYZ5678", NomeCliente = "Jane Smith" },
        new() { NumeracaoOrcamento = "334", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
      ];

      Peca[] pecas =
      [
        new() { NomePeca = "Peca1", Quantidade = 1, OrcamentoId = 1, EstoqueId = 1, PecaEntregue = false },
        new() { NomePeca = "Peca2", Quantidade = 3, OrcamentoId = 2, EstoqueId = 2, PecaEntregue = true },
        new() { NomePeca = "Peca3", Quantidade = 2, OrcamentoId = 1, EstoqueId = 3, PecaEntregue = true },
        new() { NomePeca = "Peca4", Quantidade = 1, OrcamentoId = 3, EstoqueId = 4, PecaEntregue = true },
      ];

      Estoque[] estoque =
      [
        new() { NomePeca = "Peca1", EstoquePeca = 100, TipoMovimentacao = ActionTypes.InStock },
        new() { NomePeca = "Peca2", EstoquePeca = 200, TipoMovimentacao = ActionTypes.Reserved },
        new() { NomePeca = "Peca3", EstoquePeca = 2, TipoMovimentacao = ActionTypes.Reserved },
        new() { NomePeca = "Peca4", EstoquePeca = 0, TipoMovimentacao = ActionTypes.Reserved },
      ];

      context.Pecas.AddRange(pecas);
      context.Estoque.AddRange(estoque);
      context.Orcamentos.AddRange(orcamentos);

      // save changes in DB
      context.SaveChanges();
    }
  }
}
