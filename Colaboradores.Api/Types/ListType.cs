﻿using Colaboradores.Api.Entities;
using Colaboradores.Api.Infra.Contexts;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colaboradores.Api.Types
{
    public class ListType : ObjectType<ItemList>
    {
        // since we are inheriting from objtype we need to override the functionality
        protected override void Configure(IObjectTypeDescriptor<ItemList> descriptor)
        {
            descriptor.Description("Used to group the do list item into groups");

            descriptor.Field(x => x.ItemDatas).Ignore();

            descriptor.Field(x => x.ItemDatas)
                        .ResolveWith<Resolvers>(p => p.GetItems(default!, default!))
                        .UseDbContext<ApiDbContext>()
                        .Description("This is the list of to do item available for this list");
        }

        private class Resolvers
        {
            public IQueryable<ItemData> GetItems(ItemList list, [ScopedService] ApiDbContext context)
            {
                return context.Items.Where(x => x.ListId == list.Id);
            }
        }
    }
}
