namespace cs.Domain
{
    using System.Text.Json.Serialization;

    public class Area
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Item> Items { get; set; }

        public Area()
        {
            // Initialize defaults to avoid null reference issues
            Name = "";
            Items = new List<Item>();
        }

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
            return Items?.Where(x => x.Name.ToLowerInvariant() == itemName).FirstOrDefault();
        }

        // A way to view the given area details
        public override string ToString()
        {
            return $"Area ID: {ID}, Name: {Name}";
        }
    }   
}