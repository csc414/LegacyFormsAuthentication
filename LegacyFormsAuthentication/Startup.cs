using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LegacyFormsAuthentication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication("LegacyForms").AddLegacyFormsAuthentication(options =>
            {
                options.ValidationKey = "5E5A81C32831AA920877BF0ECC7FFFCD8064E0F98D2988E48E40C3D1CB7E994395F1F784BAC5EE4FCAA222C58BC57C18C3B59956DA96ECB2F3B80CF85B48EDD8";
                options.DecryptionKey = "412C521E1EC6A8D8BE99AC5FB7DFCABA03F644690867A5F5437250BA19695F42";
                //options.LoginPath = "OldWebSite Login Uri";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
