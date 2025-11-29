namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    /// summary>
    /// A Drop Command class for dropping an item from the players inventory to the current area
    /// </summary>
    class CommandDrop : BaseCommand, ICommand 
    {
        // Uses the same structure as the other command classes in the Commands folder
        public CommandDrop()
        {
            this.description = "Smid en genstand";
        }

        // The method that executes the drop command with an if statement to check if the player has the item
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            if (parameters.Length == 0)
            {
                storyHandler._UI.DrawInfo("Brug: smid [genstand navn]");
                return;
            }
            
            // get name of item
            string itemName = base.JoinParameters(parameters);
            
            // find the ID of item
            Player player = storyHandler.GetPlayer();
            Inventory inventory = player.Inventory;
            
            Item? item = inventory.GetItem(itemName);

            // Checker for if the item value is null
            if (item == null)
            {
                storyHandler._UI.DrawError("Hov... Du har ikke denne genstand");
            }
            
            // Check if user has item
            if (item != null)
            {
                // add the item to the area the user is in
                ((ContextScene)storyHandler.GetCurrentScene()).Area.AddItem(item);
                
                // Remove the item from the players inventory
                storyHandler.Player.Inventory.RemoveItem(item);

                // Inform the user with a feedback message
                storyHandler._UI.DrawInfo($"Du smed: {itemName} [{item.Description}]");
            }

        }
    } 
}