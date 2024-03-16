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
    EstoqueDto UpdatePartById(int id, Estoque partBody); // update a part in stock by its id
    List<EstoqueDto> UpdateEstoque(List<Estoque> estoqueBody); // update an entire stock

    // CREATE
    // EstoqueDto AddPartInEstoque(Estoque estoque); // manually add a part in stock

    // DELETE
    // void RemovePartFromEstoque(int id); // remove part from stock 
  }
}
