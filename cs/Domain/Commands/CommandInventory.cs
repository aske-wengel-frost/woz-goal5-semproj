namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    using System;

    class CommandInventory : BaseCommand, ICommand
    {
        public CommandInventory()
        {
            this.description = "Viser player's inventar";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            
            Player player = StoryHandler.GetPlayer();
            if (player == null)
            {
                
                StoryHandler._UI.DrawError("FEJL: Spilleren kunne ikke findes."); //Dont know is it should be here.
                return;
            }

            
            StoryHandler._UI.DrawInfo("\n=== Dit Inventar ===");

            
            if (player.Inventory.IsEmpty())
            {
                
                StoryHandler._UI.DrawInfo("Inventar er tomt.");
            }
            else
            {
                
                foreach (Item item in player.Inventory.GetItems())
                {
                    
                    StoryHandler._UI.DrawInfo(item.ToString());
                }
            }
        }
    }
}


