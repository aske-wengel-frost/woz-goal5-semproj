namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    using System;

    /// <summary>
    /// CommandInventory class which implements the ICommand interface
    /// and provides functionality to display the player's inventory.
    /// </summary>
    class CommandInventory : BaseCommand, ICommand
    {
        public CommandInventory()
        {
            this.description = "Viser player's inventar";
        }

        // <summary>
        // The Execution of the method will displays the player's inventory
        // items to the user through the StoryHandler's UI.
        // With an appropriate design pattern for the user to understand it better.
        // </summary>
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            
            Player player = storyHandler.GetPlayer();
                  
            storyHandler._UI.DrawInfo("====[ Dit Inventar ]====");

            // If-statement to check if the inventory is empty
            if (player.Inventory.IsEmpty())
            {
                
                storyHandler._UI.DrawError("Inventar er tomt.");
            }
            else
            {
                // Loop through each item in the player's inventory and display its details
                foreach (Item item in player.Inventory.GetItems())
                {
                    
                    storyHandler._UI.DrawInfo($"* {item.ToString()}");
                }
            }
        }
    }
}


