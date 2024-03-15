using Ultracar.Dto;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    IEnumerable<OrcamentoDto> GetOrcamentos();
  }
}
