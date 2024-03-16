using Microsoft.AspNetCore.Mvc;
using Ultracar.Dto;
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
  }
}
