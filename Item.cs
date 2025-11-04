/* Item class representing an item in the Game
 */

namespace cs
{

    using System;
    using System.Collections.Generic;

    class Item
    {
        // Just simple properties for the Item class that use Getters and Setters 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor for the Item class
        public Item(int ID, string name, string description)
        {
            this.ID = ID;
            this.Name = name;
            this.Description = description;
        }

        // A way to view the given item details
        public override string ToString()
        {
            return $"Item ID: {ID}, Name: {Name}, Description: {Description}";
        }
    }
}