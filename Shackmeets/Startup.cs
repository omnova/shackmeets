using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Shackmeets.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Shackmeets.Services;

namespace Shackmeets
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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // Configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");

      services.Configure<AppSettings>(appSettingsSection);

      var appSettings = appSettingsSection.Get<AppSettings>();

      // Configure services
      services.AddScoped<IChattyService, ChattyService>();
      services.AddScoped<IGoogleMapsService, GoogleMapsService>();

      //// Configure Swagger
      //services.AddSwaggerGen(c =>
      //{
      //  //The generated Swagger JSON file will have these properties.
      //  c.SwaggerDoc("v1", new Info
      //  {
      //    Title = "Shackmeets API",
      //    Version = "v1",
      //  });

      //  //Locate the XML file being generated by ASP.NET...
      //  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
      //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

      //  //... and tell Swagger to use those XML comments.
      //  c.IncludeXmlComments(xmlPath);
      //});

      // Configure Entity Framework
      services.AddDbContext<ShackmeetsDbContext>(options => options.UseSqlServer(appSettings.ConnectionString));
      
      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
        {
          configuration.RootPath = "ClientApp/build";
        });

      // http://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management#startup-cs

      var key = Encoding.ASCII.GetBytes(appSettings.Secret);

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.Events = new JwtBearerEvents();
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();
      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseReactDevelopmentServer(npmScript: "start");
        }
      });

      ////This line enables the app to use Swagger, with the configuration in the ConfigureServices method.
      //app.UseSwagger();

      ////This line enables Swagger UI, which provides us with a nice, simple UI with which we can view our API calls.
      //app.UseSwaggerUI(c =>
      //{
      //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shackmeets API");
      //});
    }
  }
}
