using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("this is the first middle ware");
                logger.LogInformation("M1: Incoming request");
                await next();
                logger.LogInformation("M1: outgoing response");
            });

            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("this is the first middle ware");
                logger.LogInformation("M2: Incoming request");
                await next();
                logger.LogInformation("M2: outgoing response");
            });
            // hkg: this is called terminal middleware because this is not passing the flow to the next middleware
            app.Run(async (context) =>
            {
                // await context.Response.WriteAsync(_config["CustomKey"]);
                await context.Response.WriteAsync("Response from the M3 middleware!");
                logger.LogInformation("M3: processing the request and made response");
            });
        }
    }
}
