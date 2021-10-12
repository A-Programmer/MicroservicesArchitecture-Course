using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PostsService.AsyncDataServices;
using PostsService.Data;
using PostsService.SyncDataServices;

namespace PostsService
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            if(_env.IsProduction())
            {
                Console.WriteLine(" ==> Using SQL Server Db Provider.");
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("PostsConnectionString")));
            }
            else
            {
                Console.WriteLine(" ==> Using In Memory Database");
                services.AddDbContext<AppDbContext>(opt =>
                        opt.UseInMemoryDatabase("PostsDb"));
            }

            services.AddHttpClient<IUserDataClient, HttpUserDataClient>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostsService", Version = "v1" });
            });

            Console.WriteLine($"Users Service Url is: {Configuration["UsersServiceUrl"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostsService v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedDb.Seed(app, _env.IsProduction());
        }
    }
}
