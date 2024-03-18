using Ultracar.Dto;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IOrcamentoRepository
  {
    // READ
    IEnumerable<OrcamentoDto> GetOrcamentos(); // list all quotes
    IEnumerable<OrcamentoDto> GetOrcamentoById(int id); // list one quote based on its id in the database
    IEnumerable<OrcamentoDto> GetOrcamentoByName(string clientName); // list one or more quotes based on the client name
    IEnumerable<OrcamentoDto> GetOrcamentoByLicensePlate(string licensePlate); // list one or more quotes based on the clients car license plate number
    IEnumerable<OrcamentoDto> GetOrcamentoByNumber(string orcamentoNumber); // list one quote based on its identifier number

    // UPDATE
    OrcamentoDto UpdateOrcamentoInfo(int id, Orcamento orcamentoInfoBody); // update info of a single quote by its id
    OrcamentoDto UpdatePecasInOrcamento(int id, List<Peca> pecasToEditFromBody); // updates one or more parts in a client's quote by its id

    // CREATE
    InsertOrcamentoDto CreateOrcamentoInfo(Orcamento newOrcamentoInfoBody); // create initial quote info
    OrcamentoDto AddPecasInOrcamento(int id, List<Peca> pecasFromBody); // adds one or more parts in a client's quote by its id

    // DELETE
    void RemoveOrcamento(int id); // remove a quote by its id

    // HELPERS
    void HandleStock(Peca peca); // helper that checks and handle parts changes/transactions in stock 
  }
}
