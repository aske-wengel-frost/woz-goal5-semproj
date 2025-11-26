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

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            
            Player player = storyHandler.GetPlayer();
            if (player == null)
            {
                
                storyHandler._UIHandler.DrawError("FEJL: Spilleren kunne ikke findes."); //Dont know is it should be here.
                return;
            }

            
            storyHandler._UIHandler.DrawInfo("\n=== Dit Inventar ===");

            
            if (player.inventory.IsEmpty())
            {
                
                storyHandler._UIHandler.DrawInfo("Inventar er tomt.");
            }
            else
            {
                
                foreach (Item item in player.inventory.GetItems())
                {
                    
                    storyHandler._UIHandler.DrawInfo(item.ToString());
                }
            }
        }
    }
}


