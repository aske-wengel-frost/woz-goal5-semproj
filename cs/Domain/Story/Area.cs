namespace cs.Domain.Story
{
    using cs.Domain;
    using cs.Domain.Player;

    using System.Text.Json.Serialization;

    public class Area
    {
        private static int currentID = 0;

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

        public List<int> itemIds { get; set;} = new List<int>();

        [JsonIgnore]
        public Dictionary<int, Item> Items { get; private set; }

        // Frame property defines the physical representation of the area, it is nullable as every area must not have a physical representation.
        public Frame? Frame { get; set;}

        // NEEDED???
        public Area()
        {
            // Initialize defaults to avoid null reference issues
            Name = "";
            ID = GetNextId();
            Items = new Dictionary<int, Item>();
        }

        // Constructor for Area initialization
        public Area(string name, Dictionary<int,Item>? items = null, Frame? frame = null) // Area can contain a List of items and gives it a default value.  
        {
            this.ID = GetNextId();
            this.Name = name; 
            this.Frame = frame;
            Items = new Dictionary<int, Item>();
        }

        public Area AddItem(Item item)
        {
            Items.Add(item.ID, item);
            // Also add id to ids list so it is added when exporting to josn 
            itemIds.Add(item.ID);
            // this enables for chaining the method together when building stories from code
            return this;
        }

        public Item? TakeItem(string itemName)
        {
            return Items?.Values.Where(x => x.Name.ToLowerInvariant() == itemName).FirstOrDefault();
        }

        //public Item? FindItem(string itemname)
        //{

        //}

        // A way to view the given area details
        public override string ToString()
        {
            return $"Area ID: {ID}, Name: {Name}";
        }

        // Helpers
        private static int GetNextId()
        {
            return currentID++;
        }

        /// <summary>
        /// Resets the static ID counter. Used when restarting the game.
        /// </summary>
        public static void ResetIdCounter()
        {
            currentID = 0;
        }
    }   
}