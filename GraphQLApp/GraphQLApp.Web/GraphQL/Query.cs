using System;
using System.Linq;
using GraphQLApp.DataAccess;
using GraphQLApp.DataAccess.Models;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLApp.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
       // [UseProjection]
       //Si se usa este decorador sobreescribe los resolvers en base a la bd
       //Si se usa un query type tienen que comentarse
       [UseFiltering]
       [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        // [UseProjection]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }


}