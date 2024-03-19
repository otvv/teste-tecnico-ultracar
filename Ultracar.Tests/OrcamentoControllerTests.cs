using Microsoft.AspNetCore.Mvc;
using Moq;
using Ultracar.Controllers;
using Ultracar.Repository;
using Ultracar.Models;
using Ultracar.Dto;

namespace Ultracar.Tests;

public class OrcamentoControllerTests
{
  private readonly Mock<IOrcamentoRepository> _mockOrcamentoRepository;
  private readonly OrcamentoController _orcamentoController;
  public OrcamentoControllerTests()
  {
    _mockOrcamentoRepository = new Mock<IOrcamentoRepository>();
    _orcamentoController = new OrcamentoController(_mockOrcamentoRepository.Object);
  }

  [Fact]
  public void GetOrcamentosTest()
  {
    // arrange
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John Doe", PlacaVeiculo = "123ABC", Pecas = {} },
        new() { Id = 2, NumeracaoOrcamento = "222", NomeCliente = "Jane Doe", PlacaVeiculo = "ABC123", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentos()).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentos() as OkObjectResult; 

    // assert
    Assert.NotNull(result);
    Assert.Equal(200, result.StatusCode); // see if all Orcamentos got returned
    Assert.Equal(orcamentoDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetOrcamentosFailTest()
  {
    // arrange
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = [];
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentos()).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentos() as NotFoundResult;

    // assert
    Assert.Null(result); // see if the API will throw a NotFoundResult in case the Orcamento table is empty
  }
  
  [Fact]
  public void GetOrcamentoByIdTest()
  {
    // arrange
    int mockedId = 1;
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John Doe", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoById(mockedId)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoById(mockedId) as OkObjectResult; 

    // assert
    Assert.NotNull(result); // check if what got returned is not null
    Assert.Equal(200, result.StatusCode); // see if one Orcamento was returned
    Assert.Equal(orcamentoDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetOrcamentoByIdFailTest()
  {
    // arrange
    int mockedFailId = 999;
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = [];
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoById(mockedFailId)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoById(mockedFailId) as NotFoundResult;

    Console.WriteLine(result);

    // assert
    Assert.Null(result); // in theory if a orcamento is not found, the result will always be null
  }
}
