using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp
{
    public class Startup
    {        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        	services.AddDbContext<Models.AppDbContext>(options => options.UseInMemoryDatabase("appdb"));
        	//services.AddDbContext<Models.AppDbContext>(options => options.UseSqlite("FileName=app.db"));
            services.AddMvc().AddNewtonsoftJson();      
            services.AddCors(options => 
            {
                options.AddPolicy("DemoAppCorsPolicy", builder => 
                {
                    builder.AllowAnyOrigin() //WithOrigins("http://localhost:6000")
                        //.AllowAnyMethod()
                        //.AllowAnyHeader()
                        ;
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("DemoAppCorsPolicy");
			app.UseFileServer();
            app.UseRouting(routes =>
            {
                routes.MapControllers();
            });
        }
    }
}
