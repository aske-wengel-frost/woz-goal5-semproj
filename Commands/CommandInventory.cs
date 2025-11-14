namespace cs.Commands
{
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
                
                StoryHandler._UIHandler.DrawError("FEJL: Spilleren kunne ikke findes."); //Dont know is it should be here.
                return;
            }

            
            StoryHandler._UIHandler.DrawInfo("\n=== Dit Inventar ===");

            
            if (player.Inventory.IsEmpty())
            {
                
                StoryHandler._UIHandler.DrawInfo("Inventar er tomt.");
            }
            else
            {
                
                foreach (Item item in player.Inventory.GetItems())
                {
                    
                    StoryHandler._UIHandler.DrawInfo(item.ToString());
                }
            }
        }
    }
}


