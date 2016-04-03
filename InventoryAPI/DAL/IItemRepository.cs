using System;
using InventoryAPI.Domain;

namespace InventoryAPI.Repository
{
    public interface IItemRepository
    {
        int InsertItem(Item item);
        Item TakeItem(string label);
        void SimulateExpiredItems(DateTime date);
    }
}