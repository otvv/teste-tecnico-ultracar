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
        new() { Id = 2, NomePeca = "Peca2", EstoquePeca = 10, TipoMovimentacao = ActionTypes.InStock},
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
    Assert.Null(result); // see if the API will throw a NotFoundResult in case the Orcamento table is empty
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
    Assert.Equal((int)ReturnCodes.OK, result.StatusCode); // see if all Estoque got returned
    Assert.Equal(mockedResult, result.Value); // see if object returned is the same as the expected one
  }
}
