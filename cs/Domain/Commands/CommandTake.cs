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

            //JoinItemName: Combine array of words into one string
            string itemName = JoinParameters(parameters);

            Item? item = StoryHandler.GetCurrentScene().Area.TakeItem(itemName);

            // Check if the item exists
            if(item == null)
            {
                StoryHandler._UIHandler.DrawError("Denne genstand findes vidst ikke...");
                return;
            }

            // Attempt to add the item to the player's inventory.
            bool success = StoryHandler.GetPlayer().Inventory.AddItem(item);

            if (success)
            {
                // Remove the item from the Area
                StoryHandler.GetCurrentScene().Area.Items.Remove(item.ID);

                // Notify player of picked up item
                StoryHandler._UIHandler.DrawInfo($"Du samlede op: {item.Name} [{item.Description}]");
            }

            else
            {
                //The inventory was full (MaxCapacity reached)

                StoryHandler._UIHandler.DrawError("Dit inventar er fuldt! (Max 2 ting)");

                StoryHandler._UIHandler.DrawError("Brug 'Smid' kommandoen for at lave plads");
            }
        }
    }
}
