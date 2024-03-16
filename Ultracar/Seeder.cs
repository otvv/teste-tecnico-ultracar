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
      {
        new() { Id = 1, NumeracaoOrcamento = "112", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
        new() { Id = 2, NumeracaoOrcamento = "223", PlacaVeiculo = "XYZ5678", NomeCliente = "Jane Smith" }
      };

      Peca[] pecas =
      {
        new() { NomePeca = "Peca1", EstoquePeca = 100, OrcamentoId = 1, EstoqueId = 1, PecaEntregue = false },
        new() { NomePeca = "Peca2", EstoquePeca = 200, OrcamentoId = 2, EstoqueId = 2, PecaEntregue = true }
      };

      Estoque[] estoque =
      {
        new() { NomePeca = "Peca1", Quantidade = 100, TipoMovimentacao = ActionTypes.InStock },
        new() { NomePeca = "Peca2", Quantidade = 200, TipoMovimentacao = ActionTypes.Reserved }
      };

      context.Pecas.AddRange(pecas);
      context.Estoque.AddRange(estoque);
      context.Orcamentos.AddRange(orcamentos);

      // save changes in DB
      context.SaveChanges();
    }
  }
}
