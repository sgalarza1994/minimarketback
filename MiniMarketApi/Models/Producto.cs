using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarketApi.Models
{
  public class Producto
  {
    [Key]
     public int ProductoId { get; set; }
    [Column(TypeName ="varchar(100)")]
     public string Descripcion { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    public decimal Precio { get; set; }
  }
}
