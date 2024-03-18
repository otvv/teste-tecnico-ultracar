using Microsoft.AspNetCore.Mvc;
using Ultracar.Dto;
using Ultracar.Models;
using Ultracar.Repository;

namespace Ultracar.Controllers
{
  [ApiController]
  [Route("orcamentos")]
  public class OrcamentoController : Controller
  {
    private IOrcamentoRepository _repository;
    public OrcamentoController(IOrcamentoRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IActionResult GetOrcamentos()
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentos();

      // if somehow the quote table is empty or can't be found, return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return list of quotes with status code 200
      return Ok(orcamentos);
    }
    [HttpGet("{id}")]
    public IActionResult GetOrcamentoById(int id)
    {
      IEnumerable<OrcamentoDto>? orcamento = _repository.GetOrcamentoById(id);

      // if the specified quote table is empty or doesn't exist return a NotFound error.
      if (orcamento == null)
      {
        return NotFound();
      }

      // return a single quote with status code 200
      return Ok(orcamento);
    }
    [HttpGet("cliente/{clientName}")]
    public IActionResult GetOrcamentoByName(string clientName)
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentoByName(clientName);

      // if the specified quote table is empty or doesn't exist return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return a single or more quotes with status code 200
      return Ok(orcamentos);
    }
    [HttpGet("veiculo/{licensePlate}")]
    public IActionResult GetOrcamentoByLicensePlate(string licensePlate)
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentoByLicensePlate(licensePlate);

      // if the specified quote table is empty or doesn't exist return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return a single or more quotes with status code 200
      return Ok(orcamentos);
    }
    [HttpGet("numero/{orcamentoNumber}")]
    public IActionResult GetOrcamentoByNumber(string orcamentoNumber)
    {
      IEnumerable<OrcamentoDto>? orcamento = _repository.GetOrcamentoByNumber(orcamentoNumber);

      // if the specified quote table is empty or doesn't exist return a NotFound error.
      if (orcamento == null)
      {
        return NotFound();
      }

      // return a single or more quotes with status code 200
      return Ok(orcamento);
    }

    //

    [HttpPut("{id}/info/update")]
    public IActionResult UpdateOrcamentoInfo(int id, [FromBody] Orcamento orcamentoBody)
    { 
      // update an entire quote by its id
      OrcamentoDto editedResult = _repository.UpdateOrcamentoInfo(id, orcamentoBody);

      // if the quote body is empty return a BadRequest error.
      if (orcamentoBody == null)
      {
        return BadRequest();
      }

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }
    [HttpPut("{id}/peca/update")]
    public IActionResult UpdatePecasInOrcamento(int id, [FromBody] List<Peca> pecasToEditFromBody)
    {
      // if the quote body is empty return a BadRequest error.
      if (pecasToEditFromBody == null)
      {
        return BadRequest();
      }

      // edit quote with pecas from body
      OrcamentoDto orcamentoToEdit = _repository.UpdatePecasInOrcamento(id, pecasToEditFromBody);

      // return created quote as result with status code 201
      return Created($"{id}/peca/update", orcamentoToEdit);
    }

    //

    [HttpPost]
    public IActionResult CreateOrcamentoInfo([FromBody] Orcamento newOrcamentoBody)
    {
      // if the quote body is empty return a BadRequest error.
      if (newOrcamentoBody == null)
      {
        return BadRequest();
      }

      // create quote from body
      InsertOrcamentoDto orcamentoToCreate = _repository.CreateOrcamentoInfo(newOrcamentoBody);

      // return created quote as result with status code 201
      return Created("orcamentos", orcamentoToCreate);
    }
    [HttpPost("{id}/peca/add")]
    public IActionResult AddPecasInOrcamento(int id, [FromBody] List<Peca> pecasFromBody)
    {
      // if the quote body is empty return a BadRequest error.
      if (pecasFromBody == null)
      {
        return BadRequest();
      }

      // edit quote with pecas from body
      OrcamentoDto orcamentoToEdit = _repository.AddPecasInOrcamento(id, pecasFromBody);

      // return created quote as result with status code 201
      return Created($"{id}/peca/add", orcamentoToEdit);
    }

    //

    [HttpDelete("{id}")]
    public IActionResult RemoveOrcamento(int id)
    { 
      if (id == 0)
      { 
        // if the user explicitly puts 0 or "nothing" as an id, return a Bad Request error
        return BadRequest();
      }

      // attempt to remove a quote by its id
      _repository.RemoveOrcamento(id);

      // return status code 204 (No Content) in case everything goes well
      return NoContent();
    }
  }
}
