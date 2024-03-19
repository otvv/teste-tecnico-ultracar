using Microsoft.AspNetCore.Mvc;
using Moq;
using Ultracar.Controllers;
using Ultracar.Repository;
using Ultracar.Models;
using Ultracar.Dto;

namespace Ultracar.Tests;

public class EstoqueControllerTests
{
  private readonly Mock<IEstoqueRepository> _mockEstoqueRepository;
  private readonly EstoqueController _estoqueController;
  public EstoqueControllerTests()
  {
    _mockEstoqueRepository = new Mock<IEstoqueRepository>();
    _estoqueController = new EstoqueController(_mockEstoqueRepository.Object);
  }

  [Fact]
  public void GetEstoqueTest()
  {
    // arrange
    List<EstoqueDto> estoqueDtoList =
    [
        new() { Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved },
        new() { Id = 2, NomePeca = "Peca2", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock },
    ];
    IEnumerable<EstoqueDto> estoqueDtoEnumerable = estoqueDtoList;
    _mockEstoqueRepository.Setup(repository => repository.GetEstoque()).Returns(estoqueDtoEnumerable);

    // act
    var result = _estoqueController.GetEstoque() as OkObjectResult; 

    // assert
    Assert.NotNull(result);
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if all Estoque got returned
    Assert.Equal(estoqueDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetEstoqueFailTest()
  {
    // arrange
    IEnumerable<EstoqueDto> estoqueDtoEnumerable = [];
    _mockEstoqueRepository.Setup(repository => repository.GetEstoque()).Returns(estoqueDtoEnumerable);

    // act
    var result = _estoqueController.GetEstoque() as NotFoundResult;

    // assert
    Assert.Null(result); // see if the API will throw a NotFoundResult in case the Estoque table is empty
  }

  [Fact]
  public void GetPartByIdTest()
  {
    // arrange
    int mockedId = 1;
    EstoqueDto mockedResult = new()
    { 
      Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved,
    };

    _mockEstoqueRepository.Setup(repository => repository.GetPartById(mockedId)).Returns(mockedResult);

    // act
    var result = _estoqueController.GetPartById(mockedId) as OkObjectResult; 

    // assert
    Assert.NotNull(result);
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if a part got returned with the specified id
    Assert.Equal(mockedResult, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetPartByIdTestFail()
  {
    // arrange
    int mockedFailId = 999;
    EstoqueDto mockedResult = new()
    { 
      Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved,
    };

    _mockEstoqueRepository.Setup(repository => repository.GetPartById(mockedFailId)).Returns(mockedResult);

    // act
    var result = _estoqueController.GetPartById(mockedFailId) as NotFoundResult; 

    // assert
    Assert.Null(result); // if this is null it means the controller couldnt find a part with the specified id.
  }

  [Fact]
  public void GetPartByNameTest()
  {
    // arrange
    string mockedName = "Peca1";
    EstoqueDto mockedResult = new()
    { 
      Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved,
    };

    _mockEstoqueRepository.Setup(repository => repository.GetPartByName(mockedName)).Returns(mockedResult);

    // act
    var result = _estoqueController.GetPartByName(mockedName) as OkObjectResult; 

    // assert
    Assert.NotNull(result);
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if a single part got returned with the specified name
    Assert.Equal(mockedResult, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetPartByNameFail()
  {
    // arrange
    string mockedFailName = "XXX";
    EstoqueDto mockedResult = new()
    { 
      Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved,
    };

    _mockEstoqueRepository.Setup(repository => repository.GetPartByName(mockedFailName)).Returns(mockedResult);

    // act
    var result = _estoqueController.GetPartByName(mockedFailName) as NotFoundResult; 

    // assert
    Assert.Null(result); // if this is null it means the controller couldnt find a part with the specified name.
  }

  [Fact]
  public void GetPartsByStateTest()
  {
    // arrange
    ActionTypes stateMocked = ActionTypes.InStock;
    List<EstoqueDto> expectedEstoqueDtoList =
    [
        new() { Id = 2, NomePeca = "Peca2", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock },
    ];
    IEnumerable<EstoqueDto> expectedEstoqueDtoEnumerable = expectedEstoqueDtoList;
    _mockEstoqueRepository.Setup(repository => repository.GetPartsByState(stateMocked)).Returns(expectedEstoqueDtoEnumerable);

    // act
    var result = _estoqueController.GetPartByState(stateMocked) as OkObjectResult; 

    // assert
    Assert.NotNull(result);
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if all parts with specified state got returned
    Assert.Equal(expectedEstoqueDtoEnumerable, result.Value); // see if object returned is the same as the expected one
  }

  [Fact]
  public void GetPartsByStateFailTest()
  {
    // arrange
    ActionTypes stateMocked = ActionTypes.InStock;
    List<EstoqueDto> expectedEstoqueDtoList =
    [
        new() { Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved },
    ];
    IEnumerable<EstoqueDto> expectedEstoqueDtoEnumerable = expectedEstoqueDtoList;
    _mockEstoqueRepository.Setup(repository => repository.GetPartsByState(stateMocked)).Returns(expectedEstoqueDtoList);

    // act
    var result = _estoqueController.GetEstoque() as NotFoundResult;

    // assert
    Assert.Null(result); // see if the API will throw a NotFoundResult in case the part found is not the one being searched
  }

  [Fact]
  public void UpdatePartByIdTest()
  {
    // arrange
    int mockedId = 1;
    EstoqueDto originalPartDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 1,
      TipoMovimentacao = ActionTypes.InStock,
    };
    Estoque estoquePartEditBody = new() { Id = 1, NomePeca = "Correia Dentada", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock };
    EstoqueDto mockedResultDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 1,
      TipoMovimentacao = ActionTypes.InStock,
    };
    _mockEstoqueRepository.Setup(repository => repository.UpdatePartById(mockedId, estoquePartEditBody)).Returns(mockedResultDto);

    // act
    var result = _estoqueController.UpdatePartById(mockedId, estoquePartEditBody) as OkObjectResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.NotEqual(originalPartDto, result.Value); // see if object returned is different than the original
  }

  [Fact]
  public void UpdateEstoqueTest()
  {
    // arrange
    List<EstoqueDto> mockedEstoqueDtoList =
    [
        new() { Id = 1, NomePeca = "Peca1", EstoquePeca = 20, TipoMovimentacao = ActionTypes.Reserved },
        new() { Id = 2, NomePeca = "Peca2", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock },
    ];
    List<Estoque> estoqueToEditDtoList =
    [
        new() { Id = 1, NomePeca = "Correia Dentada", EstoquePeca = 20, TipoMovimentacao = ActionTypes.InStock },
        new() { Id = 2, NomePeca = "Correia Acessorios", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock },
    ];
    List<EstoqueDto> mockedEstoqueResultDtoList =
    [
        new() { Id = 1, NomePeca = "Correia Dentada", EstoquePeca = 20, TipoMovimentacao = ActionTypes.InStock },
        new() { Id = 2, NomePeca = "Correia Acessorios", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock },
    ];
    _mockEstoqueRepository.Setup(repository => repository.UpdateEstoque(estoqueToEditDtoList)).Returns(mockedEstoqueResultDtoList);

    // act
    var result = _estoqueController.UpdateEstoque(estoqueToEditDtoList) as OkObjectResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.NotEqual(mockedEstoqueDtoList, result.Value); // see if object returned is different than the original
  }

    [Fact]
  public void AddPartInEstoqueTest()
  {
    // arrange
    Estoque mockedPartToAddDto = new() 
    {
      NomePeca = "Embreagem",
      EstoquePeca = 9,
      TipoMovimentacao = ActionTypes.InStock,
    };
    EstoqueDto mockedResultDto = new() 
    {
      Id = 1,
      NomePeca = "Embreagem",
      EstoquePeca = 9,
      TipoMovimentacao = ActionTypes.InStock,
    };
    _mockEstoqueRepository.Setup(repository => repository.AddPartInEstoque(mockedPartToAddDto)).Returns(mockedResultDto);

    // act
    var result = _estoqueController.AddPartInEstoque(mockedPartToAddDto) as CreatedResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.CREATED, result.StatusCode);
  }

  [Fact]
  public void AddStockToPartByIdTest()
  {
    // arrange
    int mockedId = 1;
    int mockedQuantity = 1;
    EstoqueDto originalPartDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 9,
      TipoMovimentacao = ActionTypes.InStock,
    };
    EstoqueDto mockedResultDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 10,
      TipoMovimentacao = ActionTypes.InStock,
    };
    _mockEstoqueRepository.Setup(repository => repository.AddStockToPartById(mockedId, mockedQuantity)).Returns(mockedResultDto);

    // act
    var result = _estoqueController.AddStockToPartById(mockedId, mockedQuantity) as OkObjectResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.NotEqual(originalPartDto, result.Value); // see if object returned is different than the original
  }

  [Fact]
  public void RemoveStockFromPartByIdTest()
  {
    // arrange
    int mockedId = 1;
    int mockedQuantity = 1;
    EstoqueDto originalPartDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 10,
      TipoMovimentacao = ActionTypes.InStock,
    };
    EstoqueDto mockedResultDto = new() 
    {
      Id = 1,
      NomePeca = "Correia Acessorios",
      EstoquePeca = 9,
      TipoMovimentacao = ActionTypes.InStock,
    };
    _mockEstoqueRepository.Setup(repository => repository.RemoveStockFromPartById(mockedId, mockedQuantity)).Returns(mockedResultDto);

    // act
    var result = _estoqueController.RemoveStockFromPartById(mockedId, mockedQuantity) as OkObjectResult;

    // assert
    Assert.NotNull(result); // check if the controller returns a valid result
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode);
    Assert.NotEqual(originalPartDto, result.Value); // see if object returned is different than the original
  }

  [Fact]
  public void RemovePartFromEstoqueTest()
  {
    // arrange
    int mockedId = 1;
    _mockEstoqueRepository.Setup(repository => repository.RemovePartFromEstoque(mockedId));

    // act
    var result = _estoqueController.RemovePartFromEstoque(mockedId) as NotFoundResult;

    // assert
    Assert.Null(result); // if Orcamento gets deleted succesfully, it shouldn't return anything
  }
}
