using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//entity fr codefirst ile ve migration yap�s�yla db i tamamen kod taraf�nda olu�tu
namespace HotelFinder.API
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
            //singleton design patterni asp .net mvc core
            //i�erisinde gelen dependency injec mekanizmas�yla sa�land�
            //services.AddRazorPages();
            services.AddControllers();
            //birisi senin const�nda IHotelServ istiyosa sen ona HotelMan �ret
            services.AddSingleton<IHotelService, HotelManager>();
            //IHot isteniyorsa HotRep newle
            services.AddSingleton<IHotelRepository, HotelRepository>();
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = (doc =>
                  {
                      doc.Info.Title = "All Hotels Api";
                      doc.Info.Version = "1.0.13";
                      doc.Info.Contact = new NSwag.OpenApiContact()
                      {
                          Name = "Nurh�da �zkaya",
                          Url = "https://github.com/ozkayanurhuda",
                          Email = "ozkayanurhuda@gmail.com"
                      };
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
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //For swagger integration
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
