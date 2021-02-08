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

            descriptor.Field(p => p.Id).Type<NonNullType<IntType>>();
            descriptor.Field(p => p.Name).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor.Field(p => p.Commands)
                      .UseDbContext<AppDbContext>()
                      .ResolveWith<Resolvers>(p => Resolvers.GetCommands(default!, default!))
                      .Description("This is the list of available commands for this platform");
        }


        private class Resolvers
        {
            public static IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(x => x.PlatformId == platform.Id);
            }
        }
    }
}