/* Inventory Class to manage the collection of items in the Game
 */

namespace cs.Domain.Player
{
    using System;
    using System.Collections.Generic;

    public class Inventory
    {
        private List<Item> items;

        //Define  the maximum cpacity of the inventory. 
        private const int MaxCapacity = 2; 

        // Constructor for inventory initialization
        public Inventory()
        {
            items = new List<Item>();
        }

        // Method to add an item to the inventory
        // Returns true if succesfull, false if full or item is null 
        public bool AddItem(Item item)
        {
            //Cheks if the inventory has reached it's maximum capacity 
            if (items.Count >= MaxCapacity)
            {
                return false; //Inventory is full
            }

            if (item != null)
            {
                items.Add(item);
                return true; // Item added
            }

            return false; // Item = null
        }

        // Method to remove an item from the inventory
        //Returns true if the item was found and removed, otherwise false 
        public bool RemoveItem(Item item) 
        {
            
            if (items.Contains(item))
            {
               items.Remove(item);
               return true; // Item removed successfully
            }
            else
            {
                return false; // Item not found
            }
        }

        /// <summary>
        /// Method to get an item by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Item GetItem(int ID)
        {
            /// Foreach loop that check if the ID matches what the player is looking for
            foreach (Item item in items)
            {
                if (item.ID == ID)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Method to get an item by its Name
        /// </summary>
        public Item GetItemName(string name)
        {
            /// Foreach loop that check names with case insensitivity
            foreach (Item item in items)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Giver en "read-only" liste af alle items.
        /// 'CommandInventory' vil bruge denne.
        /// </summary>
        public IReadOnlyList<Item> GetItems()
        {
            return items.AsReadOnly();
        }

        /// <summary>
        /// En simpel hjælpemetode til at tjekke, om inventory er tom.
        /// 'CommandInventory' vil bruge denne.
        /// </summary>
        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public bool ItemExists(int? ID)
        {
            return items.Exists(_ => _.ID == ID);
        }

    }
}