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
    Assert.Equal(200, result.StatusCode);
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

    // assert
    Assert.Null(result); // in theory if an Orcamento is not found, the result will always be null
  }

  [Fact]
  public void GetOrcamentoByNameTest()
  {
    // arrange
    string mockedName = "John";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByName(mockedName)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByName(mockedName) as OkObjectResult; 

    // assert
    Assert.NotNull(result); // check if what got returned is not null
    Assert.Equal(200, result.StatusCode);
    Assert.Equal(orcamentoDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetOrcamentoByNameFailTest()
  {
    // arrange
    string mockedFailName = "";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByName(mockedFailName)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByName(mockedFailName) as NotFoundResult;

    // assert
    Assert.Null(result); // in theory if an Orcamento is not found, the result will always be null
  }

  [Fact]
  public void GetOrcamentoByLicensePlateTest()
  {
    // arrange
    string mockedLicensePlate = "123ABC";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByLicensePlate(mockedLicensePlate)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByLicensePlate(mockedLicensePlate) as OkObjectResult; 

    // assert
    Assert.NotNull(result); // check if what got returned is not null
    Assert.Equal(200, result.StatusCode);
    Assert.Equal(orcamentoDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetOrcamentoByLicensePlateFailTest()
  {
    // arrange
    string mockedFailName = "XXXXXX";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByLicensePlate(mockedFailName)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByName(mockedFailName) as NotFoundResult;

    // assert
    Assert.Null(result); // in theory if an Orcamento is not found, the result will always be null
  }
}
