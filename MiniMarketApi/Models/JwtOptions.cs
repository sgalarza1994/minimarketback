using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MiniMarketApi.Models
{
  public class JwtOptions
  {
    public bool Enabled { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int Expiration { get; set; }
    public List<Claim> Claims { get; set; }
  }
}
