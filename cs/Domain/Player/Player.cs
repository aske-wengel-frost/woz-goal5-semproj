namespace cs.Domain.Player
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int PartnerAggression { get; set; }
        public Inventory inventory { get; set; }

        // We initialize score=0, creates new Inventory for Player, assigns Player a name
        public Player(string name)
        {
            Name = name;
            Score = 0;
            inventory = new Inventory();
        }
        
        public Item? DropItem(string ItemName)
        {
            return inventory.GetItemName(ItemName);
        }

        // Increase Score by 5
        public void IncreaseScore()
        {
            Score += 5;
            //Console.WriteLine($"{Name}'s score is increased to {Score}");
        }

        // Decrease score by 5
        public void DecreaseScore()
        {
            Score -= 5;
            //Console.WriteLine($"{Name}'s score is decreased to {Score}");
        }

        // Adds Item to new initialized Inventory 
        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        // Removes Item to new initialized Inventory 
        public void RemoveItem(Item item)
        {
            inventory.RemoveItem(item);
        }

        // Displays Information about Player in Terminal
    }
}