namespace cs.Commands
{
    using System;

    class CommandInventory : BaseCommand, ICommand
    {
        public CommandInventory()
        {
            this.description = "Viser player's inventar";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to show the player's inventory
            Player player = StoryHandler.GetPlayer();
            if (player != null)
            {
                Console.WriteLine("\n=== Dit Inventory ===");
                player.Inventory.ShowInventory();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Du har ingen items"); // Debugger print
                Console.ResetColor();
            }
        }
    }
}