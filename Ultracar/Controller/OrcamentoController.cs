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

      // if somehow the Orcamento table is empty or can't be found, return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return list of orcamentos with status code 200
      return Ok(orcamentos);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrcamentoById(int id)
    {
      OrcamentoDto? orcamento = _repository.GetOrcamentoById(id);

      // if the specified orcamento table is empty or doesn't exist return a NotFound error.
      if (orcamento == null)
      {
        return NotFound();
      }

      // return a single orcamento with status code 200
      return Ok(orcamento);
    }

    [HttpGet("cliente/{clientName}")]
    public IActionResult GetOrcamentoByName(string clientName)
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentoByName(clientName);

      // if the specified orcamento table is empty or doesn't exist return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return a single or more orcamentos with status code 200
      return Ok(orcamentos);
    }

    [HttpGet("veiculo/{licensePlate}")]
    public IActionResult GetOrcamentoByLicensePlate(string licensePlate)
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentoByLicensePlate(licensePlate);

      // if the specified orcamento table is empty or doesn't exist return a NotFound error.
      if (orcamentos == null)
      {
        return NotFound();
      }

      // return a single or more orcamentos with status code 200
      return Ok(orcamentos);
    }
    [HttpGet("numero/{orcamentoNumber}")]
    public IActionResult GetOrcamentoByNumber(string orcamentoNumber)
    {
      OrcamentoDto? orcamento = _repository.GetOrcamentoByNumber(orcamentoNumber);

      // if the specified orcamento table is empty or doesn't exist return a NotFound error.
      if (orcamento == null)
      {
        return NotFound();
      }

      // return a single or more orcamentos with status code 200
      return Ok(orcamento);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrcamentoById(int id, [FromBody] Orcamento orcamento)
    { 
      // update an entire orcamento by its id
      OrcamentoDto editedResult = _repository.UpdateOrcamentoById(id, orcamento);

      // if the orcamento body is empty return a BadRequest error.
      if (orcamento == null)
      {
        return BadRequest();
      }

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }
    [HttpPut]
    public IActionResult UpdateOrcamento([FromBody] Orcamento orcamento)
    {  
      // update an entire orcamento
      OrcamentoDto editedResult = _repository.UpdateOrcamento(orcamento);

      // if the orcamento body is empty return a BadRequest error.
      if (orcamento == null)
      {
        return BadRequest();
      }

      // return partial edited data as result with status code 200
      return Ok(editedResult);
    }

    [HttpPost]
    public IActionResult CreateOrcamento([FromBody] Orcamento orcamento)
    {
      // create orcamento from body
      OrcamentoDto orcamentoToCreate = _repository.CreateOrcamento(orcamento);

      // if the orcamento body is empty return a BadRequest error.
      if (orcamento == null)
      {
        return BadRequest();
      }

      // return created orcamento as result with status code 201
      return Created("orcamento", orcamentoToCreate);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveOrcamento(int id)
    { 
      if (id == 0)
      { 
        // if the user explicitly puts 0 or "nothing" as a id return a Bad Request error
        return BadRequest();
      }

      // attempt to remove an orcamento by its id
      _repository.RemoveOrcamento(id);

      // return status code 204 (No Content) in case everything goes well
      return NoContent();
    }
  }
}
