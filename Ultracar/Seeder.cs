using Ultracar.Context;
using Ultracar.Models;

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

    // seed "orcamentos" table
    Orcamento[] orcamentos = new[]
    {
      new Orcamento { NumeracaoOrcamento = "112", PlacaVeiculo = "ABC1234", NomeCliente = "John Doe" },
      new Orcamento { NumeracaoOrcamento = "223", PlacaVeiculo = "XYZ5678", NomeCliente = "Jane Smith" },
    };

    context.Orcamentos.AddRange(orcamentos);
    
    // save changes in the DB
    context.SaveChanges();
  }
}
