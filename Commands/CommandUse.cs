namespace cs.Commands
{
    using System;

    class CommandUse : BaseCommand, ICommand
    {
        public CommandUse()
        {
            this.description = "Brug en genstand";
        }

        // Method to use item in the current scene
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            if (parameters.Length == 0)
            { 
                StoryHandler._UIHandler.DrawInfo("Brug: use [Genstand navn]");
                return;
            }

            string itemName = parameters[0];

            Item? item = StoryHandler.GetPlayer().Inventory.GetItemName(itemName);

            if (item == null)
            { 
                StoryHandler._UIHandler.DrawError($"Du har ikke '{itemName}' i dit inventar.");
                return;
            }

            bool success = StoryHandler.UseItemInScene(item);

            if (!success)
            { 
                StoryHandler._UIHandler.DrawError($"Du kan ikke bruge '{itemName}' her.");
            }
        }
    }
}