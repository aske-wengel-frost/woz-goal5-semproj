namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    /// <summary>
    /// A Drop Command class for dropping an item from the players inventory to the current area
    /// </summary>
    class CommandDrop : BaseCommand, ICommand 
    {
        public CommandDrop()
        {
            this.description = "Smid en genstand";
        }

        // The method that executes the drop command with an if statement to check if the player has the item
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            if (parameters.Length == 0)
            {
                storyHandler.UI.DrawInfo("Brug: smid [genstand navn]");
                return;
            }
            
            // Get name of item
            string itemName = base.JoinParameters(parameters);
            
            // Find the ID of item
            Player player = storyHandler.GetPlayer();
            Inventory inventory = player.Inventory;
            
            Item? item = inventory.GetItem(itemName);

            // Checks if the item value is null
            if (item == null)
            {
                storyHandler.UI.DrawError("Hov... Du har ikke denne genstand");
            }
            
            // Check if user has item
            if (item != null)
            {
                // Add the item to the area the user is in
                ((ContextScene)storyHandler.GetCurrentScene()).Area.AddItem(item);
                
                // Remove the item from the players inventory
                storyHandler.Player.Inventory.RemoveItem(item);

                // Inform the user with a feedback message
                storyHandler.UI.DrawInfo($"Du smed: {itemName} [{item.Description}]");
            }

        }
    } 
}