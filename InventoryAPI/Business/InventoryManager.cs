using InventoryAPI.Domain;
using InventoryAPI.Repository;
using System;

namespace InventoryAPI.Business
{
    //Simulate removed notification
    public delegate void RemovedItemHandler(object sender, EventArgs e);

    public class InventoryManager : IInventoryManager
    {
        private IItemRepository itemRepository;

        //Simulate expired notification
        private ExpirationHandler expiredItemHandler;

        private event RemovedItemHandler itemRemoved;

        public InventoryManager(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        /// <summary>
        /// For test purposes, simulate the notifications
        /// </summary>
        /// <param name="itemRepository"></param>
        /// <param name="expiredItemHandler"></param>
        /// <param name="removedItemHandler"></param>
        public InventoryManager(IItemRepository itemRepository, ExpirationHandler expiredItemHandler, RemovedItemHandler removedItemHandler)
        {
            this.itemRepository = itemRepository;
            this.expiredItemHandler = expiredItemHandler;
            this.itemRemoved += removedItemHandler;
        }

        public int AddItemToInventory(string label, ItemType type, DateTime? expirationDate)
        {
            try
            {
                Item itemToAdd = new Item(label, type, expirationDate);
                int id = itemRepository.InsertItem(itemToAdd);
                if (expirationDate.HasValue)
                {
                    itemToAdd.Expired += expiredItemHandler;
                }
                return id;
            }
            catch (Exception ex)
            {
                //log goes here
                throw;
            }
            finally
            {
                //code to clean unmanaged resoures goes here
            }
        }

        public Item TakeItemByLabel(string label)
        {
            try
            {
                Item takenItem = itemRepository.TakeItem(label);
                if (takenItem != null && itemRemoved != null)
                {
                    itemRemoved(takenItem, EventArgs.Empty);
                }
                return takenItem;
            }
            catch (Exception ex)
            {
                //log goes here
                throw;
            }
            finally
            {
                //code to clean unmanaged resoures goes here
            }
        }

        /// <summary>
        /// For test purposes
        /// </summary>
        /// <param name="date"></param>
        public void SimulateExpiredItems(DateTime date)
        {
            itemRepository.SimulateExpiredItems(date);
        }
    }
}
