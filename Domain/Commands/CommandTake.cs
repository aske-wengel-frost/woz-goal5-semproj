namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    using System;

    class CommandTake : BaseCommand, ICommand
    {
        public CommandTake()
        {
            this.description = "Tag en givende genstand(ene)";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Attempts to take the item with the specified name from the first parameter
            if (parameters.Length == 0)
            {
                StoryHandler._UIHandler.DrawInfo("Brug: tag [genstand navn]");
                return;
            }
            
            Item? item = StoryHandler.GetCurrentScene().Area.TakeItem(parameters[0]);

            // Check if the item exists
            if(item == null)
            {
                StoryHandler._UIHandler.DrawError("Denne genstand findes vidst ikke...");
                return;
            }

            // Add the item to the players inventory
            StoryHandler.player.Inventory.AddItem(item);

            // Remove the item from the Area
            StoryHandler.GetCurrentScene().Area.Items.Remove(item);

            // Notify player of picked up item
            StoryHandler._UIHandler.DrawInfo($"Du samlede op: {item.Name} [{item.Description}]");

        }
    }
}
