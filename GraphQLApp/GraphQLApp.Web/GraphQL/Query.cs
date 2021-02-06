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
        [UseProjection]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }


}