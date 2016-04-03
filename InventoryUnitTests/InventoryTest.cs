using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Business;
using InventoryAPI.Domain;
using InventoryAPI.Repository;

namespace InventoryUnitTests
{
    [TestClass]
    public class InventoryTest
    {
        IInventoryManager inventoryManager;
        string removedLabed;
        int expiredItemsCount;

        /// <summary>
        /// Tests' initialization.
        /// Creates a manager and adds an item
        /// </summary>
        [TestInitialize]
        public void InitBeforeEachTest()
        {
            removedLabed = null;
            expiredItemsCount = 0;

            inventoryManager = new InventoryManager(new ItemRepository(), new ExpirationHandler(ExpiredNotification), new RemovedItemHandler(RemovedNotification));

            inventoryManager.AddItemToInventory("DefaultItem", ItemType.Type1, null);
            inventoryManager.AddItemToInventory("ExpiresTodayItem1", ItemType.Type2, DateTime.Today);
            inventoryManager.AddItemToInventory("ExpiresTodayItem2", ItemType.Type2, DateTime.Today);
            inventoryManager.AddItemToInventory("ExpiresTomorrowItem1", ItemType.Type3, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        public void AddItemToRepositoryTest()
        {
            string label = "ItemType1";
            ItemType type = ItemType.Type1;
            DateTime expirationDate = DateTime.Today.AddDays(1);
            int id = inventoryManager.AddItemToInventory(label, type, expirationDate);

            Assert.AreEqual(5, id);
        }

        [TestMethod]
        public void TakeItemFromRepositoryTest()
        {
            string label = "DefaultItem";
            inventoryManager.TakeItemByLabel(label);
            Assert.AreEqual(label, removedLabed);
        }

        /// <summary>
        /// Remove notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovedNotification(object sender, EventArgs e)
        {
            Item item = sender as Item;
            Assert.IsNotNull(item);
            removedLabed = item.Label;
        }

        [TestMethod]
        public void ExpiredItemsTest()
        {
            inventoryManager.SimulateExpiredItems(DateTime.Today);
            Assert.AreEqual(2, expiredItemsCount);
        }

        /// <summary>
        /// Expired notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpiredNotification(object sender, EventArgs e)
        {
            Assert.IsInstanceOfType(sender, typeof(Item));
            expiredItemsCount++;
        }

        [TestMethod]
        public void TakeNonExistentItem()
        {
            Item item = inventoryManager.TakeItemByLabel("NonExistent");
            Assert.IsNull(item);
        }
    }
}
