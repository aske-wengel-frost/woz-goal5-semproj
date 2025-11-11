namespace cs
{
    public class Area
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Item> Items { get; set; }

        // Constructor for Area initialization
        public Area(int ID, string name, List<Item>? itmes = null ) // Area can contain a List of itmes and gives it a default value.  
        {
            this.ID = ID;
            this.Name = name; 
            //If the developer has given items, we use it else we make a empty List. 
            if ( itmes != null )
            {
                Items = itmes;
            }
            else
            {
                Items = new List<Item>();
            }
        }

        public Item? TakeItem(string itemName)
        {
            return Items?.Where(x => x.Name == itemName).FirstOrDefault();
        }

        // A way to view the given area details
        public override string ToString()
        {
            return $"Area ID: {ID}, Name: {Name}";
        }
    }   
}