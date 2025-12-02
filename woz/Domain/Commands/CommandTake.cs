namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    using System;

    class CommandTake : BaseCommand, ICommand
    {
        public CommandTake()
        {
            this.description = "Tag en givende genstand(ene)";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Attempts to take the item with the specified name from the first parameter
            if (parameters.Length == 0)
            {
                storyHandler.UI.DrawInfo("Brug: tag [genstand navn]");
                return;
            }

            //JoinItemName: Combine array of words into one string
            string itemName = JoinParameters(parameters);
            
            Item? item = ((ContextScene)storyHandler.GetCurrentScene()).Area.TakeItem(itemName);

            // Check if the item exists
            if(item == null)
            {
                storyHandler.UI.DrawError("Denne genstand findes vidst ikke...");
                return;
            }

            // Attempt to add the item to the player's inventory.
            bool success = storyHandler.GetPlayer().Inventory.AddItem(item);

            if (success)
            {
                // Remove the item from the Area
                ((ContextScene)storyHandler.GetCurrentScene()).Area.Items.Remove(item.Id);

                // Notify player of picked up item
                storyHandler.UI.DrawInfo($"Du opsamlede: {item.Name} - {item.Description}");
            }
            else
            {
                //The inventory was full (MaxCapacity reached)

                storyHandler.UI.DrawError("Dit inventar er fuldt! (Max 2 ting)");

                storyHandler.UI.DrawError("Brug 'Smid' kommandoen for at lave plads");
            }
        }
    }
}
