using InventoryAPI.Business;
using InventoryAPI.Domain;
using InventoryAPI.Repository;
using System;

namespace InventoryAPI
{
    /// <summary>
    /// Entry point, security would be added here
    /// </summary>
    public class APIEntryPoint
    {
        IInventoryManager inventoryManager;

        public APIEntryPoint()
        {
            inventoryManager = new InventoryManager(new ItemRepository());
        }

        public int Add(string label, ItemType type, DateTime? expirationDate)
        {
            return inventoryManager.AddItemToInventory(label, type, expirationDate);
        }

        public Item Take(string label)
        {
            return inventoryManager.TakeItemByLabel(label);
        }
    }
}
