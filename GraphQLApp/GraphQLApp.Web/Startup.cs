using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQLApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using GraphQLApp.GraphQL;
using GraphQL.Server.Ui.Voyager;
using GraphQLApp.GraphQL.Platforms;
using GraphQLApp.Web.GraphQL;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.AspNetCore;

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
            //Se agrega esto para evitar problemas de usar el mismo contexto en varias peticiones (paralelas)
            //Revisar si se puede meter funciones asíncronas en las querys
            services.AddPooledDbContextFactory<AppDbContext>(opt => 
                opt.UseSqlServer(_configuration.GetConnectionString("GraphQLApp")
            ));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddSubscriptionType<Subscription>()
                .AddMutationType<Mutation>()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions(); // Para producción se debe manejar de otra manera

            //.AddProjections();
            //Añadir proyecciones incluye los resolvers automaticamente de las relaciones en la bd,
            //y en tal caso de definir alguno generara problema con esta opción

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //para soportar las subscripciones de GraphQL
            app.UseWebSockets();
           

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
