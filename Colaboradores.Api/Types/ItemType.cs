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
    public class ItemType : ObjectType<ItemData>
    {
        // since we are inheriting from objtype we need to override the functionality
        protected override void Configure(IObjectTypeDescriptor<ItemData> descriptor)
        {
            descriptor.Description("Used to define todo item for a specific list");

            descriptor.Field(x => x.ItemList)
                      .ResolveWith<Resolvers>(p => p.GetList(default!, default!))
                      .UseDbContext<ApiDbContext>()
                      .Description("This is the list that the item belongs to");
        }

        private class Resolvers
        {
            public ItemList GetList(ItemData item, [ScopedService] ApiDbContext context)
            {
                return context.Lists.FirstOrDefault(x => x.Id == item.ListId);
            }
        }
    }
}
