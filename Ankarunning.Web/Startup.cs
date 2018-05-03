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
         services.AddAuthentication(
             options => options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

         services.AddMvc();

         // Add functionality to inject IOptions<T>
         services.AddOptions();

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

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}
