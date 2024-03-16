using Ultracar.Dto;
using Ultracar.Models;

namespace Ultracar.Repository
{
  public interface IEstoqueRepository
  {
    // READ
    IEnumerable<EstoqueDto> GetEstoque(); // list all parts in stock
    EstoqueDto GetPartById(int id); // list a part in stock by its id
  }
}
