using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarketApi.Models
{
  public class CompraDetalle
  {
      [Key]
      public int CompraDetalleId { get; set; }
    [ForeignKey("CompraId")]
      public int CompraId { get; set; }
    public Compra Compra { get; set; }

    [ForeignKey("ProductoId")]
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    public decimal Precio { get; set; }

 
  }
}
