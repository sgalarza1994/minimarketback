using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MiniMarketApi.Models
{
  public class Usuario : IdentityUser
  {
    [Column(TypeName = "varchar(100)")]
    public string Nombres { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Apellidos { get; set; }
    public int Tipo { get; set; }
    [Column(TypeName = "varchar(15)")]
    public string Cedula { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string Telefono { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Direccion { get; set; }

    
  }
}
