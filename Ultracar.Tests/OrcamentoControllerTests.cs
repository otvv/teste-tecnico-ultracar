using Microsoft.AspNetCore.Mvc;
using Moq;
using Ultracar.Controllers;
using Ultracar.Repository;
using Ultracar.Models;
using Ultracar.Dto;

namespace Ultracar.Tests;

public enum ReturnCodes : int 
{
  OK = 200,
  CREATED = 201,
  NOCONTENT = 204,
  NOTFOUND = 404,
  INTERNALERROR = 500,
}

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
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if all Orcamentos got returned
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
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
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
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
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
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
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
    var result = _orcamentoController.GetOrcamentoByLicensePlate(mockedFailName) as NotFoundResult;

    // assert
    Assert.Null(result); // in theory if an Orcamento is not found, the result will always be null
  }

  [Fact]
  public void GetOrcamentoByNumberTest()
  {
    // arrange
    string mockedNumber = "111";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByNumber(mockedNumber)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByNumber(mockedNumber) as OkObjectResult; 

    // assert
    Assert.NotNull(result); // check if what got returned is not null
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.Equal(orcamentoDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetOrcamentoByNumberFailTest()
  {
    // arrange
    string mockedFailNumber = "222";
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    _mockOrcamentoRepository.Setup(repository => repository.GetOrcamentoByNumber(mockedFailNumber)).Returns(orcamentoDtoEnumerable);

    // act
    var result = _orcamentoController.GetOrcamentoByNumber(mockedFailNumber) as NotFoundResult;

    // assert
    Assert.Null(result); // in theory if an Orcamento is not found, the result will always be null
  }

  [Fact]
  public void UpdateOrcamentoInfoTest()
  {
    // arrange
    int mockedId = 1;
    List<OrcamentoDto> orcamentoDtoList =
    [
        new() { Id = 1, NumeracaoOrcamento = "111", NomeCliente = "John Doe", PlacaVeiculo = "123ABC", Pecas = {} },
    ];
    IEnumerable<OrcamentoDto> orcamentoDtoEnumerable = orcamentoDtoList;
    Orcamento orcamentoInfoEditBody = new() { NumeracaoOrcamento = "334", PlacaVeiculo = "HHH1111", NomeCliente = "John Dove" };
    OrcamentoDto mockedResultDto = new() 
    {
      Id = 1,
      NumeracaoOrcamento = "334",
      NomeCliente = "John Dove",
      PlacaVeiculo = "HHH1111",
      Pecas = { }
    };
    _mockOrcamentoRepository.Setup(repository => repository.UpdateOrcamentoInfo(mockedId, orcamentoInfoEditBody)).Returns(mockedResultDto);

    // act
    var result = _orcamentoController.UpdateOrcamentoInfo(mockedId, orcamentoInfoEditBody) as OkObjectResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.NotEqual(orcamentoDtoEnumerable, result.Value); // see if object returned is different than the original
  }

  [Fact]
  public void UpdatePecasInOrcamentoTest()
  {
    // arrange
    int mockedId = 1;
    List<Peca> orcamentoPecaEditBody = 
    [ 
      new() { EstoqueId = 1, NomePeca = "Peca Edited", Quantidade = 3 } 
    ];
    OrcamentoDto mockedResult = new()
    {
      Id = 1,
      NumeracaoOrcamento = "111",
      NomeCliente = "John Doe",
      PlacaVeiculo = "123ABC",
      Pecas =
      [
        new() { EstoqueId = 1, NomePeca = "Peca Edited", Quantidade = 3 }
      ]
    };
    _mockOrcamentoRepository.Setup(repository => repository.UpdatePartsInOrcamento(mockedId, It.IsAny<List<Peca>>())).Returns(mockedResult);

    // act
    var result = _orcamentoController.UpdatePartsInOrcamento(mockedId, orcamentoPecaEditBody) as CreatedResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.CREATED, result.StatusCode);
    Assert.Equal(mockedResult, result.Value); // see if object returned is the expected
  }

  [Fact]
  public void CreateOrcamentoInfoTest()
  {
    // arrange
    Orcamento newOrcamentoInfoBody = new() { NumeracaoOrcamento = "445", PlacaVeiculo = "ABB123", NomeCliente = "Jane Doe" };
    InsertOrcamentoDto mockedResultDto = new() 
    {
      Id = 2,
      NumeracaoOrcamento = "445",
      NomeCliente = "Jane Doe",
      PlacaVeiculo = "ABB123",
    };
    _mockOrcamentoRepository.Setup(repository => repository.CreateOrcamentoInfo(newOrcamentoInfoBody)).Returns(mockedResultDto);

    // act
    var result = _orcamentoController.CreateOrcamentoInfo(newOrcamentoInfoBody) as CreatedResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.CREATED, result.StatusCode);
    Assert.Equal(mockedResultDto, result.Value); // see if object returned is the expected
  }

  [Fact]
  public void AddPartsInOrcamentoTest()
  {
    // arrange
    int mockedId = 1;
    List<Peca> orcamentoPecaCreateBody = 
    [ 
      new() { EstoqueId = 2, NomePeca = "Peca Created", Quantidade = 4 }
    ];
    OrcamentoDto mockedResult = new()
    {
      Id = 1,
      NumeracaoOrcamento = "111",
      NomeCliente = "John Doe",
      PlacaVeiculo = "123ABC",
      Pecas =
      [
        new() { EstoqueId = 1, NomePeca = "Peca Edited", Quantidade = 3 },
        new() { EstoqueId = 2, NomePeca = "Peca Created", Quantidade = 4 }
      ]
    };
    _mockOrcamentoRepository.Setup(repository => repository.AddPartsInOrcamento(mockedId, It.IsAny<List<Peca>>())).Returns(mockedResult);

    // act
    var result = _orcamentoController.AddPartsInOrcamento(mockedId, orcamentoPecaCreateBody) as CreatedResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.CREATED, result.StatusCode);
    Assert.Equal(mockedResult, result.Value); // see if object returned is the expected
  }

  [Fact]
  public void RemoveOrcamentoTest()
  {
    // arrange
    int mockedId = 1;
    _mockOrcamentoRepository.Setup(repository => repository.RemoveOrcamento(mockedId));

    // act
    var result = _orcamentoController.RemoveOrcamento(mockedId) as NotFoundResult;

    // assert
    Assert.Null(result); // if Orcamento gets deleted succesfully, it shouldn't return anything
  }
}
