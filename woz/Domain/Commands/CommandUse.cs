namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    using System;

    class CommandUse : BaseCommand, ICommand
    {
        public CommandUse()
        {
            this.description = "Brug en genstand";
        }

        // Method to use item in the current scene
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Checks if the user has provided an item name
            if (parameters.Length == 0)
            { 
                storyHandler._UI.DrawInfo("Brug: use [Genstand navn]");
                return;
            }

            string itemName = parameters[0];

            // Like TakeItem in Area.woz, it tries to find the item in the player's inventory
            Item? item = storyHandler.GetPlayer().Inventory.GetItem(itemName);

            // Check to see if the item exists in the inventory
            if (item == null)
            { 
                storyHandler._UI.DrawError($"Du har ikke '{itemName}' i dit inventar.");
                return;
            }

            // Boolean that checks if the item can be used in the current scene
            bool success = storyHandler.UseItemInScene(item);

            // If the item cannot be used, it shows an error message
            if (!success)
            { 
                storyHandler._UI.DrawError($"Du kan ikke bruge '{itemName}' her.");
            }
        }
    }
}