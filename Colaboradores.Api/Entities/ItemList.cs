using HotChocolate;
using System.Collections.Generic;

namespace Colaboradores.Api.Entities
{
    [GraphQLDescription("Used to group the do list item into groups")]
    public class ItemList
    {
        public ItemList()
        {
            ItemDatas = new HashSet<ItemData>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemData> ItemDatas { get; set; }
    }
}