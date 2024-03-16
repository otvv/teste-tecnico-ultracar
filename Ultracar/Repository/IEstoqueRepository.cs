using Ultracar.Dto;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IEstoqueRepository
  {
    // READ
    IEnumerable<EstoqueDto> GetEstoque(); // list all parts in stock
    EstoqueDto GetPartById(int id); // list a part in stock by its id
    EstoqueDto GetPartByName(string partName); // list part in stock by its name
    IEnumerable<EstoqueDto> GetPartsByState(ActionTypes state); // list one or multiple parts in stock by its state (reserved or in stock)

    // UPDATE
    EstoqueDto UpdatePartById(int id, Estoque estoque); // update a part in stock by its id
  }
}
