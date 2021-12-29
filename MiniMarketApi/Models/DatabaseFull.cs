using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniMarketApi.Models
{
  public class DatabaseFull : IdentityDbContext<Usuario>
  {

    public DatabaseFull(DbContextOptions<DatabaseFull> options)
      : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraDetalle> CompraDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      SeedDb(builder);
      base.OnModelCreating(builder);
    }


    public static void SeedDb(ModelBuilder builder)
    {
      Usuario usuario = new Usuario
      {
        Id = "b7ddd14-6340-4840-95c2-db125484e5",
        UserName = "admin",
        NormalizedUserName =  "ADMIN",
        Email = "admin@pruebas.com",
        NormalizedEmail = "ADMIN@pruebas.com",
        Cedula = "SINCEDULA",
        Telefono = "09999912912",
        Direccion = "GYA",
        LockoutEnabled = false,
        Apellidos = "ADMIN",
        Nombres = "ADMIN",
        Tipo =1

      };
      PasswordHasher<Usuario> password = new PasswordHasher<Usuario>();
      var hashed = password.HashPassword(usuario, "Admin1234");
      usuario.PasswordHash = hashed;
      builder.Entity<Usuario>().HasData(usuario);
    }
  }
}
