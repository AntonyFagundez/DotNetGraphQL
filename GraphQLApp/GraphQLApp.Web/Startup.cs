using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQLApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using GraphQLApp.GraphQL;
using GraphQL.Server.Ui.Voyager;

namespace GraphQlApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //Se agrega esto para evitar problemas de usar el mismo contexto en varias peticiones
            //Revisar si se puede meter funciones asuncronas en las querys
            services.AddPooledDbContextFactory<AppDbContext>(opt => 
                opt.UseSqlServer(_configuration.GetConnectionString("GraphQLApp")
            ));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddProjections();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });


            app.UseGraphQLVoyager(new GraphQLVoyagerOptions() 
            {
                GraphQLEndPoint = "/graphql",
                Path = "/Graphql-voyager"
            });
        }
    }
}
