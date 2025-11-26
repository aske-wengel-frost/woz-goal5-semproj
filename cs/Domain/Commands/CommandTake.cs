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

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Attempts to take the item with the specified name from the first parameter
            if (parameters.Length == 0)
            {
                storyHandler._UIHandler.DrawInfo("Brug: tag [genstand navn]");
                return;
            }

            //JoinItemName: Combine array of words into one string
            string itemName = JoinParameters(parameters);
            
            Item? item = storyHandler.GetCurrentScene().Area.TakeItem(itemName);

            // Check if the item exists
            if(item == null)
            {
                storyHandler._UIHandler.DrawError("Denne genstand findes vidst ikke...");
                return;
            }

            // Attempt to add the item to the player's inventory.
            bool success = storyHandler.GetPlayer().inventory.AddItem(item);

            // Remove the item from the Area
            storyHandler.GetCurrentScene().Area.Items.Remove(item.ID);
            if (success)
            {
                // Remove the item from the Area
                storyHandler.GetCurrentScene().Area.Items.Remove(item.ID);

                // Notify player of picked up item
                storyHandler._UIHandler.DrawInfo($"Du samlede op: {item.Name} [{item.Description}]");
            }
            else
            {
                //The inventory was full (MaxCapacity reached)

                storyHandler._UIHandler.DrawError("Dit inventar er fuldt! (Max 2 ting)");

                storyHandler._UIHandler.DrawError("Brug 'Smid' kommandoen for at lave plads");
            }
        }
    }
}
