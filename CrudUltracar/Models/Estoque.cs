using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultracar.Models;

public enum ActionTypes : int
{
  ENTRADA,
  SAIDA
}

// estoque model (many-to-one relationship db table)
public class Estoque
{
  [Key]
  public int Id { get; set; }
  [Required]
  public int PecaId { get; set; }
  [ForeignKey("PecaId")]
  public Peca? Peca { get; set; }
  [Required]
  public DateTime DataMovimentacao { get; set; }
  [Required]
  public int Quantidade { get; set; }

  // type of the stock operation
  // could be either "ENTER" or "LEAVE"
  [Required]
  public ActionTypes TipoMovimentacao { get; set; }
}

