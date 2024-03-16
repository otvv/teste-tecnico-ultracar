using Microsoft.AspNetCore.Mvc;
using Ultracar.Dto;
using Ultracar.Repository;

namespace Ultracar.Controllers
{
  [ApiController]
  [Route("estoque")]
  public class EstoqueController : Controller
  {
    private IEstoqueRepository _repository;
    public EstoqueController(IEstoqueRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IActionResult GetEstoque()
    {
      IEnumerable<EstoqueDto>? estoque = _repository.GetEstoque();

      // if somehow the stock table is empty or can't be found, return a NotFound error.
      if (estoque == null)
      {
        return NotFound();
      }

      // return list of parts in stock with status code 200
      return Ok(estoque);
    }

     [HttpGet("{id}")]
    public IActionResult GetPartById(int id)
    {
      EstoqueDto? estoque = _repository.GetPartById(id);

      // if the specified stock table is empty or doesn't exist return a NotFound error.
      if (estoque == null)
      {
        return NotFound();
      }

      // return a single part from stock with status code 200
      return Ok(estoque);
    }
  }
}
