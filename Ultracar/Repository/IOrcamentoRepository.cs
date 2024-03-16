using Ultracar.Dto;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    IEnumerable<OrcamentoDto> GetOrcamentos(); // list all Orcamentos
    OrcamentoDto GetOrcamentoById(int id); // list one Orcamento based on its id in the database
    IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName); // list one or more Orcamentos based on the client name
    IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate); // list one or more Orcamentos based on the clients car license plate number
  }
}
