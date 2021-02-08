using GraphQLApp.DataAccess.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLApp.Web.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}
