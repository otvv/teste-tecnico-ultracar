using Ultracar.Dto;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    IEnumerable<OrcamentoDto> GetOrcamentos(); // list all Orcamentos
    OrcamentoDto GetOrcamentoById(int id); // list one Orcamento based on its id in the database
  }
}
