using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood2.Data;
using OdeToFood2.Services;

namespace OdeToFood2
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();

            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            //For development only, will corrupt when multiple users add restaurants
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // For every HTTP message that arrives,
        // the code in this Configure method will define the components that respond to that request.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter greeter,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(ConfigureRoutes);

            app.Use(next =>
            {
                return async context =>
                {
                    logger.LogInformation("Request incoming");
                    if(context.Request.Path.StartsWithSegments("/mym"))
                    {
                        await context.Response.WriteAsync("Hit!!");
                        logger.LogInformation("Request handled");
                    }
                    else
                    {
                        await next(context);
                        logger.LogInformation("Response outgoing");
                    }
                };
            });

            app.UseWelcomePage(new WelcomePageOptions
            {
                Path="/wp"
            });

            app.Use(next =>
            {
                return async (context) =>
                {
                    if (context.Request.Path.StartsWithSegments("/error"))
                    {
                        throw new Exception("Error!");
                    }
                    else
                    {
                        await next(context);
                    }
                };
            });
                
                

            app.Run(async (context) =>
            {
                //var greeting = configuration["Greeting"];
                var greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //  /home/index/4
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            //throw new NotImplementedException();
        }
    }
}
