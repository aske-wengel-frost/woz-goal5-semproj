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
        private int _Id;

        public int Id
        {
            get { return _Id; }
            init 
            { 
                _Id = value;
                if (value > currentID)
                {
                    currentID = value;
                }
            }
        }
        public string Name { get; init; }
        public string Description { get; init; }

        // Constructor for the Item class
        public Item(string name, string description)
        {
            this.Id = getNextId();
            this.Name = name;
            this.Description = description;
        }

        // A way to view the given item details
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }


        // Helpers
        private static int getNextId()
        {
            return currentID++;
        }

        public static void ResetIdCounter()
        {
            currentID = 0;
        }
    }
}