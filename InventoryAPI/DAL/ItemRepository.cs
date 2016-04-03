using InventoryAPI.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InventoryAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> itemList = new List<Item>();

        public Item TakeItem(string label)
        {
            Item result = itemList.SingleOrDefault(i => i.Label == label);
            if (result != null)
            {
                itemList.Remove(result);
            }
            return result;
        }

        public int InsertItem(Item item)
        {
            int id = itemList.Any() ? itemList.Max(i => i.Id) + 1 : 1;
            item.Id = id;
            itemList.Add(item);
            return id;
        }

        public void SimulateExpiredItems(DateTime date)
        {
            var expiredItems = itemList.Where(i => i.ExpirationDate == date);
            foreach (var item in expiredItems)
            {
                item.RaiseExpired();
            }
        }
    }
}
