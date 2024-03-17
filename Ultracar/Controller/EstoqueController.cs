using Microsoft.AspNetCore.Mvc;
using Ultracar.Dto;
using Ultracar.Repository;
using Ultracar.Models;

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
    [HttpGet("peca/{partName}")]
    public IActionResult GetPartByName(string partName)
    {
      EstoqueDto? estoque = _repository.GetPartByName(partName);

      // if the specified stock table is empty or doesn't exist return a NotFound error.
      if (estoque == null)
      {
        return NotFound();
      }

      // return a single part from stock with status code 200
      return Ok(estoque);
    }
    [HttpGet("estado/{state}")]
    public IActionResult GetPartByState(ActionTypes state)
    {
      IEnumerable<EstoqueDto>? parts = _repository.GetPartsByState(state);

      // if the specified stock table is empty or doesn't exist return a NotFound error.
      if (parts == null)
      {
        return NotFound();
      }

      // return a one or multiple parts from stock with status code 200
      return Ok(parts);
    }

    //

    [HttpPut("{id}")]
    public IActionResult UpdatePartById(int id, [FromBody] Estoque partBody)
    { 
      // if the part body is empty return a BadRequest error.
      if (partBody == null)
      {
        return BadRequest();
      }

      // update a specific part by its id
      EstoqueDto editedResult = _repository.UpdatePartById(id, partBody);

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }
    [HttpPut]
    public IActionResult UpdateEstoque([FromBody] List<Estoque> estoqueBody)
    {
      // if the quote body is empty return a BadRequest error.
      if (estoqueBody == null)
      {
        return BadRequest();
      }

      // update an entire quote
      List<EstoqueDto> editedResult = _repository.UpdateEstoque(estoqueBody);

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }
    [HttpPut("peca/{id}/add")]
    public IActionResult AddStockToPartById(int id, [FromQuery] int quantity)
    { 
      // if the quantity is invalid return a BadRequest error.
      if (quantity == 0)
      {
        return BadRequest();
      }

      // update a specific part stock by its id
      EstoqueDto editedResult = _repository.AddStockToPartById(id, quantity);

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }
    [HttpPut("peca/{id}/remove")]
    public IActionResult RemoveStockFromPartById(int id, [FromQuery] int quantity)
    { 
      // if the quantity is invalid return a BadRequest error.
      if (quantity == 0)
      {
        return BadRequest();
      }

      // update a specific part stock by its id
      EstoqueDto editedResult = _repository.RemoveStockFromPartById(id, quantity);

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }

    //

    [HttpPost]
    public IActionResult AddPartInEstoque([FromBody] Estoque newPartBody)
    {
      // create quote from body
      EstoqueDto partToCreate = _repository.AddPartInEstoque(newPartBody);

      // return created quote as result with status code 201
      return Created("estoque", partToCreate);
    }

    //

    [HttpDelete("{id}")]
    public IActionResult RemovePartFromEstoque(int id)
    { 
      if (id == 0)
      { 
        // if the user explicitly puts 0 or "nothing" as an id, return a Bad Request error
        return BadRequest();
      }

      // attempt to remove a part by its id
      _repository.RemovePartFromEstoque(id);

      // return status code 204 (No Content) in case everything goes well
      return NoContent();
    }
  }
}
