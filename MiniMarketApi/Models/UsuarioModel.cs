using System;
namespace MiniMarketApi.Models
{
  public class UsuarioModel
  {
    public string Email { get; set; }
    public string Cedula { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string UserName { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Password { get; set; }
    public int Tipo { get; set; }
  }


  public class UsuarioLogin
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
  public class UsuarioResponse
  {
    public string Token { get; set; }
    public int Tipo { get; set; }
    public string Mensaje { get; set; }
    public string Descripcion { get; set; }
    public string UsuarioId { get; set; }
  }
}
