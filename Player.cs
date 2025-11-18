namespace cs
{

    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Inventory Inventory { get; set; }

        // We initialize score=0, creates new Inventory for Player, assigns Player a name
        public Player(string name)
        {
            Name = name;
            Score = 0;
            Inventory = new Inventory();
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
            Inventory.AddItem(item);
        }

        // Removes Item to new initialized Inventory 
        public void RemoveItem(Item item)
        {
            Inventory.RemoveItem(item);
        }

        // Displays Information about Player in Terminal
    }
}