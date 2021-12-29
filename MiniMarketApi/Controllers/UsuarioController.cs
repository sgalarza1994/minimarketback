using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMarketApi.Models;
using MiniMarketApi.Utilidad;

namespace MiniMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
    public UsuarioController(DatabaseFull databaseFull,
      UserManager<Usuario> userManager,
      SignInManager<Usuario> signInManager,
      JwtOptions jwtOptions)
    {
      DatabaseFull = databaseFull;
      UserManager = userManager;
      SignInManager = signInManager;
      JwtOptions = jwtOptions;
    }

    public DatabaseFull DatabaseFull { get; }
    public UserManager<Usuario> UserManager { get; }
    public SignInManager<Usuario> SignInManager { get; }
    public JwtOptions JwtOptions { get; }

    [HttpPost("[action]")]
    public async Task<ActionResult<string>> CrearUsuario(UsuarioModel usuario)
    {
      try
      {
        var newUser = await UserManager.CreateAsync(new Usuario
        {
          Apellidos = usuario.Apellidos,
          Nombres = usuario.Nombres,
          Email = usuario.Email,
          UserName = usuario.UserName,
          Telefono = usuario.Telefono,
          Cedula = usuario.Cedula,
          Direccion = usuario.Direccion,
          Tipo = usuario.Tipo

        }, usuario.Password);

        if (newUser.Succeeded)
        {
          return Ok("OK");
        }

        string error = "";
        foreach (var item in newUser.Errors)
        {
          error += $"_{item.Description}";
        }

        return Ok(error);

      }
      catch (Exception et)
      {
        return Ok("ERROR");
      }
    }


    [HttpPost("[action]")]
    public async Task<ActionResult<UsuarioResponse>> LoginUsuario(UsuarioLogin login)
    {
      try
      {

        var success = await SignInManager.PasswordSignInAsync(login.UserName,
         login.Password, true, false);

        if (success.Succeeded)
        {
          var usuario = await DatabaseFull.Usuarios.Where(t => t.UserName == login.UserName).FirstOrDefaultAsync();
          string token = CreateToken.Create(JwtOptions);
          return Ok(new UsuarioResponse
          {
            Token = token,
            Tipo = Convert.ToInt32(usuario.Tipo),
            Mensaje = "OK",
            Descripcion = "NADA",
            UsuarioId = usuario.Id
          });
        }
         
        else
          return Ok(new UsuarioResponse
          {
            Token = "",
            Tipo = 0,
            Mensaje = "ERROR",
            Descripcion = "CREDENCIALES INCORRECTAS"
          });
      }
      catch (Exception et)
      {;
        return Ok(new UsuarioResponse
        {
          Token = "",
          Tipo = 0,
          Mensaje = "ERROR",
          Descripcion = et.Message
        });
      }
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> ObtenerCliente()
    {
      try
      {
        var cliente = await DatabaseFull.Usuarios.Where(t => t.Tipo == 2)
                     .Select(t => new
                     {
                       usuarioId = t.Id,
                       nombres = $"{t.Nombres} {t.Apellidos}"

                     }).ToListAsync();
        return Ok(cliente);
      }
      catch (Exception)
      {
        return Ok();
      }
    }
  }
}