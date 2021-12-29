using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarketApi.Models
{
  public class Compra
  {
    [Key]
    public int CompraId { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaAprobacion { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    public decimal SubTotal { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Iva { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public string Courier { get; set; }
    public string Tracking { get; set; }
    public string Observacion { get; set; }
    public string Factura { get; set; }
    [Column(TypeName = "varchar(1)")]
    public string Estado { get; set; }
    [Column(TypeName ="nvarchar(450)")]
    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }


    public List<CompraDetalle> Items { get; set; }
  }
}
