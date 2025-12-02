using NUnit.Framework;
using woz.Domain.Player;
using woz;
using System.Collections.Generic;

namespace UnitTests
{
    public class InventoryTests
    {
        // Define variables that we will reuse in multiple tests
        private Inventory inventory;
        private Item mobil;
        private Item pandekage;
        private Item toiletNøgle;

        // The code is performing Setup and teardown operations. 
        [SetUp]
        public void Setup()
        {
            // Initialize a new empty inventory
            inventory = new Inventory();

            // Create dummy items (also the items in game)  to use for testing
            mobil = new Item("Mobil", "En smartphone");
            pandekage = new Item("Pandekage", "MMMM mums en pandekage");
            toiletNøgle = new Item("Toilet Nøgle", "En nøgle til toilettet");
        }

        // TEST 1: Add items 
        // We want to verify that adding an item works when the inventory is empty.
        [Test]
        public void AddItem_WhenSpaceAvailable_ReturnsTrue()
        {
            // ACT: Try to add the first item
            bool result = inventory.AddItem(mobil);

            // ASSERT: Verify the results
            // 1. The method should return true 
            Assert.IsTrue(result, "Adding an item should return true when space is available.");

            // 2. The inventory count should now be 1
            Assert.AreEqual(1, inventory.GetItems().Count, "Inventory count should be 1.");

            // 3. The item in the inventory should be the mobil we added
            Assert.AreEqual(mobil, inventory.GetItems()[0], "The item in the inventory should be the mobil.");
        }

        // TEST 2: Max Capacity 
        // We want to verify that we CAN'T exceed the max capacity of 2.
        [Test]
        public void AddItem_WhenInventoryIsFull_ReturnsFalse()
        {
            // ARRANGE: Fill the inventory to its maximum capacity (2 items)
            inventory.AddItem(mobil);  // Item 1
            inventory.AddItem(pandekage); // Item 2 (Now full)

            // ACT: Try to add a 3rd item (The toilet nøgle)
            bool result = inventory.AddItem(toiletNøgle);

            // ASSERT: Verify that the system rejected the item
            // 1. The method should return false
            Assert.IsFalse(result, "Should return false because inventory capacity (2) is reached.");

            // 2. The count should STILL be 2, not 3
            Assert.AreEqual(2, inventory.GetItems().Count, "Inventory count should remain at 2.");

            // 3. Verify that the potion is NOT in the list
            // (The IsFalse check means: "It is false that the list contains the toilet nøgle")
            Assert.IsFalse(inventory.GetItems().Contains(toiletNøgle), "The toilet nøgle should not have been added.");
        }

        // TEST 3: Removing Items
        // We want to verify that removing an item works correctly.
        [Test]
        public void RemoveItem_WhenItemExists_ReturnsTrue()
        {
            // ARRANGE: Add an item so we have something to remove
            inventory.AddItem(mobil);

            // ACT: Try to remove that item
            bool result = inventory.RemoveItem(mobil);

            // ASSERT: Verify success
            // 1. Should return true because the item was there
            Assert.IsTrue(result, "Should return true when removing an existing item.");

            // 2. The inventory should be empty again
            Assert.IsTrue(inventory.IsEmpty(), "Inventory should be empty after removing the only item.");
        }
    }
}
