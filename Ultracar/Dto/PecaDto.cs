namespace Ultracar.Dto 
{
  public class PecaDto 
  {
    public int Id { get; set; }
    public int? EstoqueId { get; set; }
    public string? NomePeca { get; set; }
    public int Quantidade { get; set; }
    public bool PecaEntregue { get; set; }
  }
}
