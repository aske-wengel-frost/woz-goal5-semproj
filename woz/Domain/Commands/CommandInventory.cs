namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    using System;

    /// <summary>
    /// CommandInventory class provides functionality to display the player's inventory.
    /// </summary>
    class CommandInventory : BaseCommand, ICommand
    {
        public CommandInventory()
        {
            this.description = "Viser dit inventar";
        }

        /// <summary>
        /// The Execution of the method will display the player's inventory
        /// items to the user through the StoryHandler's UI.
        /// </summary>
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            Player player = storyHandler.GetPlayer();
            storyHandler.UI.DrawInventory(player.Inventory);
        }
    }
}


