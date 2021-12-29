using System;
using System.Collections.Generic;

namespace MiniMarketApi.Models
{
  public class CompraModel
  {
    public string UsuarioId { get; set; }
    public string Courier { get; set; }
    public string Tracking { get; set; }
    public string Observacion { get; set; }
    public string Factura { get; set; }
    public List<CompraDetalleModel> Items { get; set; }
  }

  public class CompraDetalleModel
  {
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
  }

  public class CompraFiltro
  {
    public string UsuarioId { get; set; }
  }

  public class CompraDespachar
  {
    public int CompraId { get; set; }
  }
}
