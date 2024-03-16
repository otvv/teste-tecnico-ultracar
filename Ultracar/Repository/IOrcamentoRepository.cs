using Ultracar.Dto;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    // READ
    IEnumerable<OrcamentoDto> GetOrcamentos(); // list all Orcamentos
    OrcamentoDto GetOrcamentoById(int id); // list one Orcamento based on its id in the database
    IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName); // list one or more Orcamentos based on the client name
    IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate); // list one or more Orcamentos based on the clients car license plate number
    OrcamentoDto GetOrcamentoByNumber(string orcamentoNumber); // list one Orcamento based on its identifier number

    // UPDATE
    OrcamentoDto UpdateOrcamentoById(int id, Orcamento orcamento); // update a specific Orcamento by its id
    OrcamentoDto UpdateOrcamento(Orcamento orcamento); // update an Orcamento

    // CREATE
    OrcamentoDto CreateOrcamento(Orcamento orcamento); // manually create an Orcamento
  }
}
