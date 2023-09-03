using Colaboradores.Api.Queries;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using GraphQL.Server.Ui.Voyager;
using Colaboradores.Api.Infra.Contexts;
using Colaboradores.Api.Types;

namespace Colaboradores.Api
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
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Projects;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                //Environment.GetEnvironmentVariable("DefaultConnection"); 
            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(
                    connectionString
                    , b => b.MigrationsAssembly("Colaboradores.Api"))
                );

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<ItemType>()
                .AddType<ListType>()
                .AddProjections()
                .AddSorting()
                .AddFiltering();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            });
        }
    }
}
