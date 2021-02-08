using System.Linq;
using GraphQLApp.DataAccess;
using GraphQLApp.DataAccess.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLApp.GraphQL.Platforms
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any command in platform.");

            descriptor.Field(c => c.Id).Type<NonNullType<IntType>>();
            descriptor.Field(c => c.HowTo).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.CommandLine).Type<NonNullType<StringType>>();

            descriptor.Field(c => c.Platform)
                      .ResolveWith<Resolvers>(p => Resolvers.GetPlatform(default!, default!))
                      .UseDbContext<AppDbContext>()
                      .Description("This is the platform that depends");
        }


        private class Resolvers
        {
            public static Platform GetPlatform(Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }
}