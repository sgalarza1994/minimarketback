using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MiniMarketApi.Models;

namespace MiniMarketApi.Utilidad
{
  public static class CreateToken
  {
    public static string Create(JwtOptions configuration)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(configuration.Issuer,
          configuration.Audience,
          expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration.Expiration)),
          signingCredentials: creds,
          claims: configuration.Claims);

      string _token = new JwtSecurityTokenHandler().WriteToken(token);

      return _token;
      //0999356302  cabana merita 
    }
  }
}
