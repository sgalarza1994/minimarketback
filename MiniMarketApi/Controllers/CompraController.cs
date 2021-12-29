using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMarketApi.Models;

namespace MiniMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
    public CompraController(DatabaseFull databaseFull)
    {
      DatabaseFull = databaseFull;
    }

    public DatabaseFull DatabaseFull { get; }


    [HttpPost("[action]")]
    public async Task<ActionResult<string>> CrearCompra(CompraModel model)
    {
      try
      {
        var subtotal = model.Items.Sum(t => t.Cantidad * t.Precio);
        var iva = subtotal * 0.12M;
        var response = new Compra
        {
           FechaAprobacion = DateTime.Now,
           FechaRegistro = DateTime.Now,
           SubTotal = subtotal,
           Total =  subtotal + iva,
           UsuarioId = model.UsuarioId,
           Iva = iva,
           Courier = model.Courier,
           Factura = model.Factura,
           Observacion = model.Observacion,
           Tracking = model.Tracking,
           Estado = "R",
           Items = model.Items.Select(t=> new CompraDetalle
           {

             Cantidad = t.Cantidad,
             Precio = t.Precio,
             ProductoId = t.ProductoId,
             
           }).ToList()
          
        };
        await DatabaseFull.Compras.AddAsync(response);
        await DatabaseFull.SaveChangesAsync();
        return Ok("OK");
      }
      catch (Exception)
      {
        return Ok("ERROR");
      }
    }


    [HttpPost("[action]")]
    public async Task<ActionResult<List<object>>> ObtenerCompraFiltro(CompraFiltro compra)
    {
      try
      {
        var response = await DatabaseFull.Compras.Include(t => t.Items).ThenInclude(t=>t.Producto)
                      .Include(t => t.Usuario)
                      .Where(t => (t.UsuarioId.Equals(compra.UsuarioId) ||
                      string.IsNullOrWhiteSpace(compra.UsuarioId)))
                      .Select(t => new
                      {
                          fechaRegistro = t.FechaRegistro.ToString("yyyy-MM-dd"),
                          t.Courier,
                          t.Tracking,
                          t.Observacion,
                          t.CompraId,
                          t.Factura,
                          t.SubTotal,
                          t.Iva ,
                          t.Total,
                          estado = t.Estado == "R" ? "GENERADA" : t.Estado == "D" ? "DESPACHADA" : "RECHAZADA",
                          usuario  = $"{t.Usuario.Nombres} {t.Usuario.Apellidos}",
                          items = t.Items.Select(p=> new
                          {
                             descripcion = p.Producto.Descripcion,
                             p.Cantidad,
                             p.Precio

                          }).ToList()

                      }).ToListAsync();
        return Ok(response);

      }
      catch (Exception et)
      {
        return Ok("ERROR");
      }
    }


    [HttpPost("[action]")]
    public async Task<ActionResult<string>> Despachar(CompraDespachar despachar)
    {
      try
      {
        var rsp = await DatabaseFull.Compras.Where(t => t.CompraId == despachar.CompraId).FirstOrDefaultAsync();
        rsp.Estado = "D";
        await DatabaseFull.SaveChangesAsync();
        return Ok("OK");
      }
      catch (Exception)
      {
        return Ok("ERROR");
      }
    }
  }
}