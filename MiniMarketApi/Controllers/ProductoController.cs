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
    public class ProductoController : ControllerBase
    {
    public ProductoController(DatabaseFull databaseFull )
    {
      DatabaseFull = databaseFull;
    }

    public DatabaseFull DatabaseFull { get; }

    [HttpGet("[action]/{productoId}")]
    public async Task<ActionResult<Producto>> ObtenerProducto(int productoId)
    {
      var response = await DatabaseFull.Productos.Where(t => t.ProductoId == productoId).FirstOrDefaultAsync();
      return Ok(response);
    }
    [HttpGet("[action]")]
    public async Task<ActionResult<List<Producto>>> ObtenerProductoAll()
    {
      var response = await DatabaseFull.Productos.ToListAsync();
      return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<string>> GuardarProducto(Producto producto)
    {
      
      try
      {
        var productoDb = new Producto
        {
          Precio = producto.Precio,
          Descripcion = producto.Descripcion
        };
        await DatabaseFull.Productos.AddAsync(productoDb);
        await DatabaseFull.SaveChangesAsync();
        return Ok("OK");
      }
      catch (Exception)
      {
        return Ok("ERROR");
      }
    }

    [HttpPut("[action]")]
    public async Task<ActionResult<string>> EditarProducto(Producto producto)
    {
      try
      {
        var productoDb = await DatabaseFull.Productos.Where(t => t.ProductoId == producto.ProductoId).FirstOrDefaultAsync();
        productoDb.Descripcion = producto.Descripcion;
        producto.Precio = producto.Precio;

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