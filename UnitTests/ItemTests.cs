using cs.Domain.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class ItemTests
    {
        // Define variables reuse from InventoryTests.cs - Which will be use for multiple tests
        private Item mobil;
        private Item pandekage;
        private Item toiletNøgle;

        public void Setup()
        {
            // Created dummy items (also the items in actual game) to use for testing
            mobil = new Item("Mobil", "En smartphone");
            pandekage = new Item("Pandekage", "MMMM mums en pandekage");
            toiletNøgle = new Item("Toilet Nøgle", "En nøgle til toilettet");
        }

        // TEST 1: Constructor
        // Verifying that the constructor sets all properties correctly
        public void Item_Constructor_SetsPropertiesCorrectly()
        {
            // ACTON: Create a new item
            Item testItem = new Item("Test Item", "A test description");

            // ASSERT: Verify the results
            // 1. The name should be set correctly
            Assert.AreEqual("Test Item", testItem.Name, "Item name should be 'Test Item'.");

            // 2. The description should be set correctly
            Assert.AreEqual("A test description", testItem.Description, "Item description should be 'A test description'.");

            // 3. The ID should be assigned (not negative)
            Assert.GreaterOrEqual(testItem.ID, 0, "Item ID should be 0 or greater.");
        }

        // TEST 2: ToString Method
        // Verification that ToString() returns the correct format.
        public void Item_ToString_ReturnsCorrectFormat()
        {
            // Using the mobil item from Setup method
            // ACT: Call ToString()
            string result = mobil.ToString();

            // ASSERT: Verify the format
            // The expected format should be: "Item ID: {ID}, Name: {Name}, Description: {Description}"
            string expectedStart = $"Item ID: {mobil.ID}, Name: Mobil, Description: En smartphone";

            Assert.AreEqual(expectedStart, result, "ToString should return the correct format.");
        }

        // TEST 3: Empty Name and Description
        public void Item_EmptyNameAndDescription_CreatesSuccessfully()
        {
            // ACT: Create an item with empty strings
            Item emptyItem = new Item("", "");
        
            Assert.AreEqual("", emptyItem.Name, "Item name should be empty string.");
        
            Assert.AreEqual("", emptyItem.Description, "Item description should be empty string.");

            Assert.GreaterOrEqual(emptyItem.ID, 0, "Item should still have a valid ID.");
        }
    }
}
