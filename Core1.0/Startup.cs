using Core1._0.Dbcontext;
using Core1._0.IServices;
using Core1._0.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core1._0
{
    public class Startup
    {

        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var secrebyte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "lyc.com",
                        ValidateAudience = true,
                        ValidAudience = "lyc.com",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secrebyte)
                    };
                }
                 );
            //options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        ValidateIssuer = true,



            //}


            services.AddControllers();
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            //services.AddTransient<ITouristRoutePicturesRepostiory,Touis>
            //services.AddDbContext(options => { options.UseSqlServer(Configuration["ConnectionStrings:ConnectionString"]); });
            services.AddDbContext<Appdbcontext>(options => { options.UseSqlServer(_configuration["ConnectionStrings:ConnectionString"]); });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapControllers();
            });
        }
    }
}
