namespace cs.Commands
{
    using System;

    class CommandInventory : BaseCommand, ICommand
    {
        public CommandInventory()
        {
            this.description = "Show the player's inventory";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to show the player's inventory
            Player player = StoryHandler.GetPlayer();
            if (player != null)
            {
                Console.WriteLine("\n=== Your Inventory ===");
                player.Inventory.ShowInventory();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You currently have no items"); // Debugger print
                Console.ResetColor();
            }
        }
    }
}