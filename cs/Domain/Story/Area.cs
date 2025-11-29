namespace cs.Domain.Story
{
    using cs.Domain;
    using cs.Domain.Player;
    using System.Text.Json.Serialization;

    public class Area
    {
        private static int currentID = 0;

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

        public List<int> itemIds { get; init;} = new List<int>();

        [JsonIgnore]
        public Dictionary<int, Item> Items { get; init; }

        // Frame property defines the physical representation of the area, it is nullable as every area must not have a physical representation.
        public Frame? Frame { get; init;}

        // NEEDED???
        public Area()
        {
            // Initialize defaults to avoid null reference issues
            Name = "";
            Id = GetNextId();
            Items = new Dictionary<int, Item>();
        }

        // Constructor for Area initialization
        public Area(string name, Dictionary<int,Item>? items = null, Frame? frame = null) // Area can contain a List of items and gives it a default value.  
        {
            this.Id = GetNextId();
            this.Name = name; 
            this.Frame = frame;
            Items = new Dictionary<int, Item>();
        }

        public Area AddItem(Item item)
        {
            Items.Add(item.Id, item);
            // Also add id to ids list so it is added when exporting to josn 
            itemIds.Add(item.Id);
            // this enables for chaining the method together when building stories from code
            return this;
        }

        public Item? TakeItem(string itemName)
        {
            return Items?.Values.Where(x => x.Name.ToLowerInvariant() == itemName).FirstOrDefault();
        }

        // A way to view the given area details
        public override string ToString()
        {
            return $"Area ID: {Id}, Name: {Name}";
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