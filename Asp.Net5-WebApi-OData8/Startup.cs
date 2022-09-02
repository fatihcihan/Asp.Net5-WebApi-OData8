using Asp.Net5_WebApi_OData8.Data.Context;
using Asp.Net5_WebApi_OData8.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net5_WebApi_OData8
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
            services.AddControllers().AddOData(option => option
                  .Select()
                  .Filter()
                  .Count()
                  .OrderBy()
                  .Expand()
                  .SetMaxTop(100)
                  .AddRouteComponents("api", GetEdmModel()));

            services.AddDbContext<EfContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EfContext"));
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asp.Net5_WebApi_OData8", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asp.Net5_WebApi_OData8 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            var gadget = modelBuilder.EntitySet<Gadget>("GadgetsOdata").EntityType.HasKey(x => x.Id);
            //gadget.Collection.Action("GetExample").Returns<IQueryable<Gadget>>();

            var category = modelBuilder.EntitySet<Category>("Categories").EntityType.HasKey(x => x.Id);
            //category.Collection.Function("GetExample").Returns<IActionResult>();
            category.Collection.Action("GetExample").Returns<IActionResult>();


            return modelBuilder.GetEdmModel();
        }
    }
}
