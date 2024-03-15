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

    //

    public IActionResult GetOrcamentos()
    {
      IEnumerable<OrcamentoDto>? orcamentos = _repository.GetOrcamentos();

      // return list of users with status code 200
      return Ok(orcamentos);
    }
  }
}
