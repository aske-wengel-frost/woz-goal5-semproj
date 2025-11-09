namespace cs.Commands
{
    using System;

    class CommandTake : BaseCommand, ICommand
    {
        public CommandTake()
        {
            this.description = "Tag en givende item(s)";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to take the viewed item in the current scene

            Item item = StoryHandler.GetCurrentScene().Area.Items.Where(x => x.Name == parameters[0]).FirstOrDefault();

            StoryHandler.player.Inventory.AddItem(item);

            StoryHandler.GetCurrentScene().Area.Items.Remove(item);

            StoryHandler._UIHandler.DrawError($"Took item: {item.Name} [{item.Description}]");

        }
    }
}