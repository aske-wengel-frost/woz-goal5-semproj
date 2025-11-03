/* Inventory Class to manage the collection of items in the Game
 */

using System;
using System.Collections.Generic;

class Inventory
{ 
    private List<Item> items;

    // Constructor for inventory initialization
    public Inventory()
    {
        items = new List<Item>();
    }

    // Method to add an item to the inventory
    public void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            Console.WriteLine($"Item: {item.Name} Added to Inventory"); //Debugger message to check
        }
    }

    // Method to remove an item from the inventory
    public void RemoveItem(Item item)
    {
        // Check control for item existence (Remove if-statement later if needed)
        if (items.Contains(item))
        {
            items.Remove(item);
            Console.WriteLine($"Item: {item.Name} Removed from Inventory"); // Debugger message to check
        }
        else
        {
            Console.WriteLine($"Item not found in inventory."); //Also a debugger message
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


    public void SetItem(int ID, Item item)
    {
        item.ID = ID;
        items.Add(item);
        Console.WriteLine($"Item: {item.Name}. Placed in the scene at position {ID}"); // Print debugger message
    }

    public void ShowInventory()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }
        else
        {
            foreach (Item item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}