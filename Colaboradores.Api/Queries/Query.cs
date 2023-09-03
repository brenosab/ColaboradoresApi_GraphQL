using System.Linq;
using Colaboradores.Api.Entities;
using HotChocolate.Data;
using HotChocolate;
using Colaboradores.Api.Infra.Contexts;

namespace Colaboradores.Api.Queries
{
    public class Query
    {
        // So basically this attribute is pulling a db context from a pool
        // using the db context 
        // returning the db context to the pool
        [UseDbContext(typeof(ApiDbContext))]
        [UseProjection] //=> we have remove it since we have used explicit resolvers
        [UseFiltering]
        [UseSorting]
        public IQueryable<ItemData> GetItems([ScopedService] ApiDbContext context)
        {
            return context.Items;
        }
        [UseDbContext(typeof(ApiDbContext))]
        [UseProjection] //=> we have remove it since we have used explicit resolvers
        [UseFiltering]
        [UseSorting]
        public IQueryable<ItemList> GetLists([ScopedService] ApiDbContext context)
        {
            return context.Lists;
        }
    }
}
