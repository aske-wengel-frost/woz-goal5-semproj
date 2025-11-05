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
            // Implement inventory display logic here
            // Note to self: Might want to access StoryHandler's player inventory
        }
    }
}