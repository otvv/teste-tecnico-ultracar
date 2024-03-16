using Ultracar.Dto;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    // READ
    IEnumerable<OrcamentoDto> GetOrcamentos(); // list all quotes
    OrcamentoDto GetOrcamentoById(int id); // list one quote based on its id in the database
    IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName); // list one or more quotes based on the client name
    IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate); // list one or more quotes based on the clients car license plate number
    OrcamentoDto GetOrcamentoByNumber(string orcamentoNumber); // list one quote based on its identifier number

    // UPDATE
    OrcamentoDto UpdateOrcamentoById(int id, Orcamento orcamentoBody); // update a specific quotes by its id
    OrcamentoDto UpdateOrcamento(Orcamento orcamentoBody); // update a quote

    // CREATE
    OrcamentoDto CreateOrcamento(Orcamento newOrcamentoBody); // manually create a quote

    // DELETE
    void RemoveOrcamento(int id); // remove a quote by its id
  }
}
