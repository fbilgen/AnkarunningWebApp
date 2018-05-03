using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ankarunning.Data;
using Microsoft.EntityFrameworkCore;
using Ankarunning.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Ankarunning.Web
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
         //register context
         services.AddDbContext<AnkarunningContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));


         // Add authentication services
         services.AddAuthentication(options =>
         {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
         })
         .AddCookie()
         .AddOpenIdConnect(options =>
         {
            options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
            options.ClientId = Configuration["Auth0:ClientId"];
            options.ClientSecret = Configuration["Auth0:ClientSecret"];
            options.ResponseType = "code";
            options.CallbackPath = new PathString("/Home");
            options.ClaimsIssuer = "Auth0";
            options.Events = new OpenIdConnectEvents
            {

               OnRedirectToIdentityProviderForSignOut = context =>
               {
                  context.Response.Redirect(options.Authority + "/v2/logout?client_id=" + options.ClientId + "&returnTo={context.Request.Scheme}://{context.Request.Host}/");
                  context.HandleResponse();

                  return Task.FromResult(0);
               },
               OnTicketReceived = context =>
               {
                  var opts = context.Options as OpenIdConnectOptions;

                  // Get the ClaimsIdentity
                  if (context.Principal.Identity is ClaimsIdentity identity)
                  {
                     // Add the Name ClaimType. This is required if we want User.Identity.Name to actually return something!
                     if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.Name) &&
                         identity.HasClaim(c => c.Type == "name"))
                        identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value, ClaimValueTypes.String, opts.Authority));

                     // Add the groups as roles
                     var authzClaim = context.Principal.FindFirst(c => c.Type == "authorization");
                     if (authzClaim != null)
                     {
                        var authorization = JsonConvert.DeserializeObject<Auth0Authorization>(authzClaim.Value);
                        if (authorization != null)
                        {
                           foreach (var group in authorization.Groups)
                           {
                              identity.AddClaim(new Claim(ClaimTypes.Role, group, ClaimValueTypes.String, opts.Authority));
                           }
                        }
                     }
                  }

                  return Task.FromResult(0);
               }
            };
         });

         services.AddMvc();


         //Repository Pattern service registration
         services.AddScoped(typeof(IAnkarunningRepository<>), typeof(AnkarunningRepository<>));
         services.AddTransient<ITrainingService<Training>, TrainingService>();
         services.AddTransient<IPhotoService<TrainingPhoto>, TrainingPhotoService>();
         services.AddTransient<IEventService<Event>, EventService>();
         services.AddTransient<IPhotoService<EventPhoto>, EventPhotoService>();
         services.AddTransient<IRouteService<Route>, RouteService>();

         // Transient: A new instance of the type is used every time the type is requested.
         // Scoped: A new instance of the type is created the first time it’s requested within a given HTTP request, 
         // and then re-used for all subsequent types resolved during that HTTP request.
         // Singleton: A single instance of the type is created once, and used by all subsequent requests for that type

         services.AddCors();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
         }

         app.UseCors(builder =>
         builder.AllowAnyOrigin());

         app.UseStaticFiles();

         app.UseAuthentication();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}
