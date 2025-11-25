/* Item class representing an item in the Game
 */

namespace cs.Domain.Player
{

    using System;
    using System.Collections.Generic;

    public class Item
    {
        private static int currentID = 0;

        // Just simple properties for the Item class that use Getters and Setters 
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set 
            { 
                _ID = value;
                if (value > currentID)
                {
                    currentID = value;
                }
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor for the Item class
        public Item(string name, string description)
        {
            this.ID = getNextId();
            this.Name = name;
            this.Description = description;
        }

        // A way to view the given item details
        public override string ToString()
        {
            return $"Item ID: {ID}, Name: {Name}, Description: {Description}";
        }


        // Helpers
        private static int getNextId()
        {
            return currentID++;
        }
    }
}