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
    IEnumerable<EstoqueDto> GetPartsByState(ActionTypes state); // list one or multiple parts in stock by its state (reserved, in stock or out of stock)

    // UPDATE
    EstoqueDto UpdatePartById(int id, Estoque partBody); // update a part in stock by its id
    List<EstoqueDto> UpdateEstoque(List<Estoque> estoqueBody); // update an entire stock

    // CREATE
    EstoqueDto AddPartInEstoque(Estoque newPartBody); // manually add a part in stock
    EstoqueDto AddStockToPartById(int id, int quantity); // manually add stock to a part
    EstoqueDto RemoveStockFromPartById(int id, int quantity); // manually remove stock from a part

    // DELETE
    void RemovePartFromEstoque(int id); // removes a specific part from the stock
  }
}
