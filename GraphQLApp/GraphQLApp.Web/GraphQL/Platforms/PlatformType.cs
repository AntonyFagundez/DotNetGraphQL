using System;
using System.Linq;
using GraphQLApp.DataAccess;
using GraphQLApp.DataAccess.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLApp.GraphQL.Platforms 
{
    public class PlatformType: ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a comand line interface.");

            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor.Field(p => p.Commands)
                      .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                      .UseDbContext<AppDbContext>()
                      .Description("This is the list of available commands for this platform");
        }


        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                Console.WriteLine(platform.Id);
                
                return context.Commands.Where(x => x.PlatformId == platform.Id);
            }
        }
    }
}