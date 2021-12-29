using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MiniMarketApi.Models;

namespace MiniMarketApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var jwtConfiguration = Configuration.GetSection("jwt").Get<JwtOptions>();
      services.AddSingleton(jwtConfiguration);
      services.AddControllers();

      services.AddDbContext<DatabaseFull>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("administracion"));
        options.EnableSensitiveDataLogging();


      }, ServiceLifetime.Transient);

      services.AddDefaultIdentity<Usuario>(options =>
      {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;

      }).AddEntityFrameworkStores<DatabaseFull>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Facturacion.WebApi", Version = "v1" });
      });
      AddCors(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Facturacion.WebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("MyPolicy");
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
    public static void AddCors(IServiceCollection services)
    {
      services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
      {

        builder.WithOrigins(
            "https://localhost:8080",
            "http://localhost:8080")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();


      }));
    }
  }
}
