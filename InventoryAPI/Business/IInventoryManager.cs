using System;
using InventoryAPI.Domain;

namespace InventoryAPI.Business
{
    public interface IInventoryManager
    {
        int AddItemToInventory(string label, ItemType type, DateTime? expirationDate);
        void SimulateExpiredItems(DateTime date);
        Item TakeItemByLabel(string label);
    }
}