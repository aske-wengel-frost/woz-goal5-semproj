/* Area class which holds the areas used in the Game
*/

using System;
using System.Collections.Generic;

public class Area
{ 
    public int ID { get; set; }
    public string Name { get; set; }

    public List<Item> item;

     // Constructor for Area initialization
    public Area(int ID, string name)
    { 
        this.ID = ID;
        this.Name = name;
        item = new List<Item>(); 

    }

    // A way to view the given area details
    public override string ToString()
    {
        return $"Area ID: {ID}, Name: {Name}";
    }
}